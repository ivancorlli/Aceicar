import IProduct from "@/lib/interface/IProduct"
import axios from "axios"
import { useMutation, useQuery, useQueryClient } from "react-query"

async function getProducts({ companyId }: { companyId?: string}): Promise<IProduct[]> {
    try {

        const res = await axios.get(`/api/company/products/${companyId}`)
        if (res.data == null || res.data == undefined) return []
        return res.data
    } catch (e) {
        return []
    }
}


function useProducts(companyId?: string ) {
    const { data, isLoading, error } = useQuery(["company-products", companyId], () => getProducts({ companyId: companyId}), { enabled: !!companyId })
    return {
        products: data,
        isLoading,
        error
    }
}



export {
    useProducts
}