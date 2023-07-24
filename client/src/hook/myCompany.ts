import ICompanyLogged from "@/lib/interface/ICompanyLogged"
import axios from "axios"
import { useMutation, useQuery, useQueryClient } from "react-query"

interface IFetcherError {
    ok: boolean,
    redirect: boolean
}

async function getCompanyLogged(): Promise<ICompanyLogged | IFetcherError | null> {
    try {
        const res = await fetch("/api/access/company/logged")
        const data = await res.json()
        if (data == null || data == undefined) return null
        return data
    } catch (e: any) {
        return null
    }
}

const useCompanyLogged = () => {
    const query = useQuery({ queryKey: "company-logged", queryFn: getCompanyLogged })
    const redirect: IFetcherError | null = query.data as IFetcherError | null ?? null
    return {
        company: <ICompanyLogged>query.data ?? null,
        isLoading: query.isLoading,
        error: query.error,
        redirect: redirect != null && redirect.redirect == true ? true : false
    }
}

async function postContactData(form:{companyId:string,email:string,country:string,number:string}) {
    await axios.post("/api/company/contact-data",form)
}
function usePostContact() {
    const mutation = useMutation(
        (form: {companyId:string,email:string,country:string,number:string}) => {
            return postContactData(form)
        })
        const queryClient = useQueryClient()
        function handlePost(form: {companyId:string,email:string,country:string,number:string}) {
            mutation.mutate(form, {
                onSuccess() {
                    queryClient.invalidateQueries(["company-logged"])
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
async function postLocation(form:{companyId:string,country:string,city:string,state:string,postalCode:string,street:string,streetNumber:string,floor?:string,apartment?:string}) {
    await axios.post("/api/company/location",form)
}
function usePostCompanyLocation() {
    const mutation = useMutation(
        (form: {companyId:string,country:string,city:string,state:string,postalCode:string,street:string,streetNumber:string,floor?:string,apartment?:string}) => {
            return postLocation(form)
        })
        const queryClient = useQueryClient()
        function handlePost(form: {companyId:string,country:string,city:string,state:string,postalCode:string,street:string,streetNumber:string,floor?:string,apartment?:string}) {
            mutation.mutate(form, {
                onSuccess() {
                    queryClient.invalidateQueries(["company-logged"])
                },
            })
        }
    
        return {
            isLoading: mutation.isLoading,
            isSuccess: mutation.isSuccess,
            data: mutation.data,
            isError: mutation.isError,
            error: mutation.error ?? "Se produjo un error al guardar datos de contacto",
            post: handlePost
        } 
}



export {
    useCompanyLogged,
    usePostContact,
    usePostCompanyLocation
}