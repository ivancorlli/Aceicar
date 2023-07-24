import { IUserAccess } from "@/lib/interface/IUserAccess";
import { getSession,withApiAuthRequired } from "@auth0/nextjs-auth0";
import axios from "axios";
import { NextApiRequest, NextApiResponse } from "next";

async function handler(req:NextApiRequest,res:NextApiResponse)
{
    const session = await getSession(req,res);
    if(session == null || session == undefined){
        res.status(401).json({ok:false,message:"Unathorized"})
        return;
    }
    if(session)
    {
        try{
            const {userId} = req.query
            const data = await axios<IUserAccess[]>({
                url:`${process.env.API_URL}/accesses/users/${userId}`,
                method:"GET",
                headers:{
                    Accept:"application/json",
                    Authorization:`Bearer ${session.accessToken}`
                },
            })
            if(data.status == 200)
            {
                res.status(200).json(data.data)
                return;
            }
        }catch(e)
        {
            res.status(500).json({ok:false,message:"Error on Api server"})
            return;
        }
    }
    res.status(401).json({ok:false,message:"Unathorized"})
    return;
}

export default  withApiAuthRequired(handler)