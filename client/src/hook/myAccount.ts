import IUser from "@/lib/interface/IUser"
import axios from "axios"
import { useMutation, useQuery, useQueryClient } from "react-query"

async function fetcher(): Promise<IUser | null> {
    const res = await fetch("/api/user/me")
    const data = await res.json()
    if (data == null || data == undefined) return null
    return data.user
}

const useAccount = () => {
    const { data, isLoading, error } = useQuery({ queryKey: "me", queryFn: fetcher })
    return {
        user: data ?? null,
        isLoading,
        error
    }
}


async function postAccount(form: { username: string, phoneCountry: string, phoneNumber: string }) {
        return await axios.post("/api/user/account", form)
}

function usePostAccount() {
    const mutation = useMutation(
        (form: { username: string, phoneCountry: string, phoneNumber: string }) => {
            return postAccount(form)
        })
    const queryClient = useQueryClient()
    function handlePost(form: { username: string, phoneCountry: string, phoneNumber: string }, fn?: () => void) {
        mutation.mutate(form, {
            onSuccess() {
                if (fn != undefined) fn()
                queryClient.invalidateQueries(["me"])
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

async function postProfile(form: { name: string, surname: string, gender: string, birth: string }) {
    return await axios.post("/api/user/profile", form)
}

function usePostProfile() {
    const mutation = useMutation(
        (form: { name: string, surname: string, gender: string, birth: string }) => {
            return postProfile(form)
        })
        const queryClient = useQueryClient()
        function handlePost(form: { name: string, surname: string, gender: string,birth:string }, fn?: () => void) {
            mutation.mutate(form, {
                onSuccess() {
                    if (fn != undefined) fn()
                    queryClient.invalidateQueries(["me"])
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
    usePostProfile
}