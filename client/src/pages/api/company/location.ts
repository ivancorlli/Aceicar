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
            const {companyId} = req.body
            const data = await axios({
                url: `${process.env.API_URL}/companies/${companyId}/location`,
                method: "PATCH",
                headers: {
                    Accept: "application/json",
                    Authorization: `Bearer ${session.accessToken}`
                },
                data: {
                   country:req.body.country,
                   city:req.body.city,
                   state:req.body.state,
                   postalCode:req.body.postalCode,
                   street:req.body.street,
                   streetNumber:req.body.streetNumber,
                   floor:req.body.floor,
                   apartment:req.body.apartment
                }
            })
            if (data.status == 200) {
                res.status(201).json({ ok: true, message: "Company created" })
                return;
            }
            res.status(200).json({ ok: true, message: "Company created" })
            return;
        } catch (e) {
            res.status(400).json({ ok: false, message: "Error on Api server" })
            return;
        }
    }
    res.status(400).json({ ok: false, message: "Unathorized" })
    return;
}

export default withApiAuthRequired(handler)