import { IUserAccess } from "@/lib/interface/IUserAccess"
import { useMutation, useQuery, useQueryClient } from "react-query"

async function fetcher(userId?:string): Promise<IUserAccess[]> {
    try{

        const res = await fetch(`/api/access/${userId}`)
        const data = await res.json()
        if (data == null || data == undefined) return []
        return data
    }catch(e)
    {
        return []
    }
}


function useAccess(userId?:string){
    const { data, isLoading, error } =  useQuery(["userAccess",userId],()=>fetcher(userId),{enabled:!!userId})
    return {
        access: data,
        isLoading,
        error
    }
}



export  {
    useAccess
}