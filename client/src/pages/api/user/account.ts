import { getSession } from "@auth0/nextjs-auth0";
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
            const data = await axios({
                url:`${process.env.API_URL}/users/${req.body.userId}/account`,
                method:"PATCH",
                headers:{
                    Accept:"application/json",
                    Authorization:`Bearer ${session.accessToken}`
                },
                data:{
                    username:req.body.username,
                    phoneCountry:req.body.phoneCountry,
                    phoneNumber:req.body.phoneNumber
                }
            })
            if(data.status == 200)
            {
                res.status(200).json({ok:true,message:"Account modified"})
                return;
            }
        }catch(e)
        {
            res.status(400).json({ok:false,message:""})
            return;
        }
    }
    res.status(400).json({ok:false,message:""})
    return;
}

export default handler