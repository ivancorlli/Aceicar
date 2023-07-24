import { getSession, withApiAuthRequired } from "@auth0/nextjs-auth0";
import axios from "axios";
import { NextApiRequest, NextApiResponse } from "next";

async function handler(req: NextApiRequest, res: NextApiResponse) {
    const session = await getSession(req, res);
    if (session == null || session == undefined) {
        res.status(401).json({ ok: false, message: "Unathorized" })
        return;
    }
    if (session) {
        try {
            const data = await axios({
                url: `${process.env.API_URL}/companies`,
                method: "POST",
                headers: {
                    Accept: "application/json",
                    Authorization: `Bearer ${session.accessToken}`
                },
                data: {
                    TypeId: req.body.typeId,
                    SpecializationId: req.body.specializationId,
                    Owner: req.body.userId,
                    Name: req.body.name
                }
            })
            if (data.status == 200) {
                res.status(201).json({ ok: true, message: "Company created" })
                return;
            }
            res.status(200).json({ ok: true, message: "Company created" })
            return;
        } catch (e) {
            res.status(400).json({ ok: false, message: "Error on Api  server" })
            return;
        }
    }
    res.status(400).json({ ok: false, message: "Unathorize" })
    return;
}

export default withApiAuthRequired(handler)