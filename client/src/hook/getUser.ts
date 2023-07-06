import IUser from "@/lib/interface/IUser"
import { useQuery } from "react-query"

async function fetcher():Promise<IUser|null> {
    const res = await fetch("/api/user/getMe")
    const data = await res.json()
    if (data == null || data == undefined) return null
    return data
}

const getUser = () => {
    const {data,isLoading,error} = useQuery({queryKey:"user",queryFn:fetcher})
    return {
        user:data,
        isLoading,
        error
    }
}

export default getUser