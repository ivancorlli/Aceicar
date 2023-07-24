import { getSession, withApiAuthRequired } from "@auth0/nextjs-auth0";
import axios from "axios";
import { NextApiRequest, NextApiResponse } from "next";
import { serialize } from "cookie";
import ICompanyAccess from "@/lib/interface/ICompanyAccess";

async function handler(req: NextApiRequest, res: NextApiResponse) {

    const { accessId } = req.query
    const invalidCookie = serialize("_CAS", JSON.stringify(""), {
        path: "/",
        httpOnly:true,
        maxAge: 0
    })
    if (accessId) {
        const session = await getSession(req, res);
        if (session == null || session == undefined) {
            res.setHeader("set-cookie", invalidCookie)
            res.redirect(`${process.env.AUTH0_BASE_URL}/`)
            return;
        }
        try {
            const data = await axios<ICompanyAccess>({
                url: `${process.env.API_URL}/accesses/${accessId}`,
                method: "GET",
                headers: {
                    Authorization: `Bearer ${session.accessToken}`
                }
            })
            if (data.data != null) {
                const validCookie = serialize("_CAS", JSON.stringify({ accessId }), {
                    path: "/",
                    httpOnly:true,
                    maxAge: 60 * 60
                })
                res.setHeader("set-cookie", validCookie)
                return res.redirect(`${process.env.AUTH0_BASE_URL}/dashboard`)
            } else {
                res.setHeader("set-cookie", invalidCookie)
                res.redirect(`${process.env.AUTH0_BASE_URL}/`)
                return
            }
        } catch (e) {
            res.setHeader("set-cookie", invalidCookie)
            res.redirect(`${process.env.AUTH0_BASE_URL}/`)
            return;
        }
    }
    res.setHeader("set-cookie", invalidCookie)
    return res.redirect(`${process.env.AUTH0_BASE_URL}/`)

}

export default withApiAuthRequired(handler)