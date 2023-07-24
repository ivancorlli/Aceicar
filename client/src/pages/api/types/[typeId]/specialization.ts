import ISpecialization from "@/lib/interface/ISpecialization";
import { getSession, withApiAuthRequired } from "@auth0/nextjs-auth0";
import axios from "axios";
import { NextApiRequest, NextApiResponse } from "next";

async function handler(req: NextApiRequest, res: NextApiResponse) {

    const {typeId} = req.query
    const session = await getSession(req, res);
    if (session == null || session == undefined) {
        res.status(401).json({ ok: false, redirect: true })
        return;
    }
    if (session) {
        try {
            const data = await axios<ISpecialization[]>({
                url: `${process.env.API_URL}/types/${typeId}/specialization`,
                method: "GET",
                headers: {
                    Authorization: `Bearer ${session.accessToken}`
                }
            })
            if (data.status == 200) {
                res.status(200).json(data.data)
                return;
            }
        } catch (e) {
            res.status(500).json({ ok: false, redirect: true })
            return;
        }
    } else {
        res.status(400).send({ ok: false, redirect: true })
        return
    }

}

export default withApiAuthRequired(handler)