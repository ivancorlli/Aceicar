'use client'
import IUser from '@/lib/interface/IUser'
import { useUser } from '@auth0/nextjs-auth0/client'
import { redirect } from 'next/navigation'
import { useQuery } from 'react-query'

async function fetcher():Promise<IUser|null> {
    const res = await fetch("/api/user/me")
    const data = await res.json()
    if (data == null || data == undefined) return null
    return data.user
}



function IndexPage(){
    const {user:session} = useUser()
    const {data:user} = useQuery<IUser | null>(["me",session],fetcher,{enabled:!!session});
    if (user != null) {
        console.log(user)
        if (user.Username == undefined || user.Phone == undefined) return redirect("/quickstart")
        if (user.Profile == undefined) return redirect("/quickstart?num=1")
    } else {
        return (
            <div>IndexPage</div>
        )
    }
}

export default IndexPage