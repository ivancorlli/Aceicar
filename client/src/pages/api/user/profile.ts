import { UserGender } from "@/lib/enum/UserGender";
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
            let gender:string = ""
            if(req.body.gender == UserGender.Male) gender = "Male"
            if(req.body.gender == UserGender.Female) gender = "Female"
            const data = await axios({
                url:"http://localhost:5000/api/user/profile",
                method:"POST",
                headers:{
                    Accept:"application/json",
                    Authorization:`Bearer ${session.accessToken}`
                },
                data:{
                    name:req.body.name,
                    surname:req.body.surname,
                    gender:gender,
                    birth:new Date(req.body.birth)
                }
            })
            if(data.status == 200)
            {
                res.status(200).json({ok:true,message:"Profile modified"})
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