import { IUserAccess } from "@/lib/interface/IUserAccess";
import { getSession, withApiAuthRequired } from "@auth0/nextjs-auth0";
import axios from "axios";
import { parse, serialize } from "cookie";
import { NextApiRequest, NextApiResponse } from "next";

async function handler(req: NextApiRequest, res: NextApiResponse) {

    const cookie = req.cookies["_CAS"]
    if (cookie) {
        const invalidCookie = serialize("_CAS", JSON.stringify(""), {
            path: "/",
            httpOnly: true,
            maxAge: 0
        })
        res.setHeader("set-cookie", invalidCookie)
        res.redirect(`${process.env.AUTH0_BASE_URL}/`)
        return
    } else {
        res.redirect(`${process.env.AUTH0_BASE_URL}/`)
        return
    }
}

export default handler