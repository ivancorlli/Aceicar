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
                url:`${process.env.API_URL}/users/${req.body.userId}/location`,
                method:"PATCH",
                headers:{
                    Accept:"application/json",
                    Authorization:`Bearer ${session.accessToken}`
                },
                data:{
                    country:req.body.country,
                    city:req.body.city,
                    state:req.body.state,
                    postalCode:req.body.postalCode,
                    street:req.body.street,
                    streetNumber:req.body.streetNumber
                }
            })
            if(data.status == 200)
            {
                res.status(200).json({ok:true,message:"Profile modified"})
                return;
            }
        }catch(e)
        {
            res.status(500).json({ok:false,message:"Error on Api server"})
            return;
        }
    }
    res.status(400).json({ok:false,message:"Unathorized"})
    return;
}

export default handler