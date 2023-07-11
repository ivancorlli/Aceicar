import User from "@/lib/entity/User";
import IUser from "@/lib/interface/IUser";
import { getAccessToken, getSession } from "@auth0/nextjs-auth0";
import axios from "axios";
import { NextApiRequest, NextApiResponse } from "next";



async function handler(req: NextApiRequest, res: NextApiResponse) {
    const session = await getSession(req, res);
    if (session == null || session == undefined) {
        res.status(500).json({ ok: false, user: null })
        return;
    }
    if (session) {
        try {
            const data = await axios({
                url: "http://localhost:5000/api/user/me",
                method: "GET",
                headers: {
                    Authorization: `Bearer ${session.accessToken}`
                }
            })

            if (data.data != null) {
                let user: User = new User(data.data.user.id, data.data.user.email)
                user.Picture = data.data.user.picture
                user.Username = data.data.user.username
                user.Phone = {
                    Country:data.data.user.phone.phoneCountry,
                    Number:data.data.user.phone.phoneNumber,
                    Verified:true
                }
                res.status(200).json({ ok: true, user })
                return;

            } else {
                res.status(400).json({ ok: false, user: null })
            }
        } catch (e) {
            res.status(400).json({ ok: false, user: null })
            return;
        }
    }
    res.status(400).json({ ok: false, user: null })
    return;
}

export default handler