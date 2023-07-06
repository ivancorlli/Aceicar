import IUser from "@/lib/interface/IUser";
import { getSession, withApiAuthRequired } from "@auth0/nextjs-auth0";
import { NextApiRequest, NextApiResponse } from "next";




async function handler(req:NextApiRequest,res:NextApiResponse)
{
    const session = await getSession(req,res);
    if(session == null || session == undefined){
        res.status(500)
        return;
    }
    if(session.user)
    {
        let user:IUser = {
            UserId: session.user.sub,
            Email: session.user.email,
        };
        if(session.user.picture)
        {
            user.Picture = session.user.picture
        }
        if(session.user.given_name && session.user.family_name)
        {
            user.Name = session.user.given_name
            user.Surname = session.user.family_name
        }
        res.status(200).json(user)
        return;
    }
    res.status(400)
    return;
}

export default handler