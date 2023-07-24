import IMyAccount from "@/lib/interface/IMyAccount";
import { getSession } from "@auth0/nextjs-auth0";
import axios from "axios";
import { NextApiRequest, NextApiResponse } from "next";



async function handler(req: NextApiRequest, res: NextApiResponse) {
    const session = await getSession(req, res);
    if (session == null || session == undefined) {
        res.status(500).json({ ok: false, message: "Unathorized" })
        return;
    }
    if (session.accessToken) {
        try {
            const data = await axios<IMyAccount>({
                url: `${process.env.API_URL}/users/me`,
                method: "GET",
                headers: {
                    Authorization: `Bearer ${session.accessToken}`
                }
            })
            if (data.data != null) {
                res.status(200).json(data.data)
                return;
                
            } else {
                res.status(400).json({ ok: false, user: null })
            }
        } catch (e) {
            res.status(500).json({ ok: false, message: "Error on api server" })
            return;
        }
    }
    res.status(400).json({ ok: false, message: "Unathorized" })
    return;
}

export default handler