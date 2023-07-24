'use client'
import IMyAccount from '@/lib/interface/IMyAccount'
import { useUser } from '@auth0/nextjs-auth0/client'
import { redirect } from 'next/navigation'
import { useQuery } from 'react-query'

async function fetcher(): Promise<IMyAccount | null> {
    const res = await fetch("/api/user/me")
    const data = await res.json()
    if (data == null || data == undefined) return null
    return data.user
}

function IndexPage() {
    const { user: session } = useUser()
    const { data: user } = useQuery<IMyAccount | null>(["my-account", session], fetcher, { enabled: !!session });
    if (session != null) {
        if (user) {
            if (user.username == undefined || user.phone == undefined) return redirect("/quickstart")
            if (user.profile == undefined) return redirect("/quickstart?num=1")
            if (user.location == undefined) return redirect("/quickstart?num=2")
        }
    }
    return (
        <div>IndexPage</div>
    )

}

export default IndexPage