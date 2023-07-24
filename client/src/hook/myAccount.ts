import IMyAccount from "@/lib/interface/IMyAccount"
import { useUser } from "@auth0/nextjs-auth0/client"
import axios from "axios"
import { useMutation, useQuery, useQueryClient } from "react-query"

async function getMyAccount(): Promise<IMyAccount | null> {
    try{

        const res = await fetch("/api/user/me")
        const data = await res.json()
        if (data == null || data == undefined) return null
        return data
    }catch(e)
    {
        return null
    }
}

const useAccount = () => {
    const { data, isLoading, error } = useQuery(["my-account"],getMyAccount)
    return {
        user: data ?? null,
        isLoading,
        error
    }
}


async function postAccount(form: { username: string, phoneCountry: string, phoneNumber: string,userId:string}) {
        return await axios.post("/api/user/account", form)
}

function usePostAccount() {
    const mutation = useMutation(
        (form: { username: string, phoneCountry: string, phoneNumber: string,userId:string }) => {
            return postAccount(form)
        })
    const queryClient = useQueryClient()
    function handlePost(form: { username: string, phoneCountry: string, phoneNumber: string, userId:string }) {
        mutation.mutate(form, {
            onSuccess() {
                queryClient.invalidateQueries(["my-account"])
            },
        })
    }

    return {
        isLoading: mutation.isLoading,
        isSuccess: mutation.isSuccess,
        data: mutation.data,
        isError: mutation.isError,
        error: mutation.error,
        post: handlePost
    }
}

async function postProfile(form: { name: string, surname: string, gender: string, birth: string,userId:string }) {
    return await axios.post("/api/user/profile", form)
}

function usePostProfile() {
    const mutation = useMutation(
        (form: { name: string, surname: string, gender: string, birth: string,userId:string }) => {
            return postProfile(form)
        })
        const queryClient = useQueryClient()
        function handlePost(form: { name: string, surname: string, gender: string,birth:string,userId:string }) {
            mutation.mutate(form, {
                onSuccess() {
                    queryClient.invalidateQueries(["my-account"])
                },
            })
        }
    
        return {
            isLoading: mutation.isLoading,
            isSuccess: mutation.isSuccess,
            data: mutation.data,
            isError: mutation.isError,
            error: mutation.error,
            post: handlePost
        } 
}
async function postLocation(form: { country: string, city: string, state: string, postalCode: string,street?:string,streetNumber?:string }) {
    return await axios.post("/api/user/location", form)
}

function usePostLocation() {
    const mutation = useMutation(
        (form: { country: string, city: string, state: string, postalCode: string,street?:string,streetNumber?:string }) => {
            return postLocation(form)
        })
        const queryClient = useQueryClient()
        function handlePost(form: { country: string, city: string, state: string, postalCode: string,street?:string,streetNumber?:string }) {
            mutation.mutate(form, {
                onSuccess() {
                    queryClient.invalidateQueries(["my-account"])
                },
            })
        }
    
        return {
            isLoading: mutation.isLoading,
            isSuccess: mutation.isSuccess,
            data: mutation.data,
            isError: mutation.isError,
            error: mutation.error,
            post: handlePost
        } 
}
export {
    useAccount,
    usePostAccount,
    usePostProfile,
    usePostLocation
}