import ICompanyAccess from "@/lib/interface/ICompanyAccess";
import ICompanyLogged from "@/lib/interface/ICompanyLogged";
import { IUserAccess } from "@/lib/interface/IUserAccess";
import { getSession, withApiAuthRequired } from "@auth0/nextjs-auth0";
import axios from "axios";
import { NextApiRequest, NextApiResponse } from "next";

async function handler(req: NextApiRequest, res: NextApiResponse) {

    const cookie = req.cookies["_CAS"]
    if (cookie) {
        const accessId = JSON.parse(cookie).accessId!
        const session = await getSession(req,res);
        if(session == null || session == undefined){
            res.status(401).json({ok:false,redirect:true})
            return;
        }
        if(session)
        {
            try{
                const data = await axios<ICompanyAccess>({
                    url: `${process.env.API_URL}/accesses/${accessId}`,
                    method: "GET",
                    headers: {
                        Authorization: `Bearer ${session.accessToken}`
                    }
                })
                if(data.status == 200)
                {
                    res.status(200).json(data.data)
                    return;
                }
            }catch(e)
            {
                res.status(500).json({ok:false,redirect:true})
                return;
            }
        }else {
            res.status(400).send({ok:false,redirect:true})
            return  
        }
    } else {
        res.status(400).send({ok:false,redirect:true})
        return 
    }
}

export default withApiAuthRequired(handler)