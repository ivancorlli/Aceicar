import CompanyHorizontal from '@/component/Card/CompanyHorizontal'
import SearchSelect, { SearchVariant, SelectOptions } from '@/component/Input/SearchSelect'
import { useCompanyLogged, usePostCompanyLocation } from '@/hook/myCompany'
import { getLocation } from '@/hook/useLocationApi'
import { Button, HStack, Input, Spinner, VStack, useToast } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { ChangeEvent, MouseEvent, useEffect, useState } from 'react'

interface IForm {
    country: string,
    city: string,
    state: string,
    postalCode: string,
    street: string,
    streetNumber: string,
    floor: string,
    apartment: string,
    companyId: string
}

const StepFive = () => {
    const router = useRouter()
    const { company } = useCompanyLogged();
    const {post,isLoading,isError,isSuccess} = usePostCompanyLocation()
    const toast = useToast()
    const [form, setForm] = useState<IForm>({
        companyId: company ? company.companyId : "",
        country: company ? company.location?.country ?? "" : "",
        city: company ?company.location?.city ?? "":"",
        state: company? company.location?.state ?? "":"",
        postalCode:company? company.location?.postalCode ?? "":"",
        street: company?company.location?.street ?? "":"",
        streetNumber:company? company.location?.streetNumber ?? "":"",
        floor: company ? company.location?.floor ?? "" :"",
        apartment: company ? company.location?.apartment ?? "" : ""
    });

    useEffect(()=>{
        setForm({
            ...form,
            companyId : company ? company.companyId : ""
        })
    },[company])

    useEffect(()=>{
        if(isSuccess)
        {
            toast({
                title: "Se guardaron los datos correctamente",
                status: "success",
                position: "top",
                isClosable: true,
            })
            router.push("/dashboard")
        }
        if(isError)
        {
            toast({
                title: "Se produjo un erro al guardar los datos",
                status: "error",
                position: "top",
                isClosable: true,
            })
        }
    },[isSuccess,isError])

    function handleNext(e: MouseEvent<HTMLButtonElement>) {
        e.preventDefault()
        post(form)
    }

    async function onSelect(e: SelectOptions): Promise<void> {
        const data = await fetch(e.value);
        const res = await data.json()
        setForm({
            ...form,
            country: res._links["city:country"].name,
            city: res._links["city:admin1_division"].name,
            state: res.name,
        })
    }

    function handleChange(e: ChangeEvent<HTMLInputElement>) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        })
    }
    return (
        <VStack alignItems="center" w="100%" spacing={8}>
            <CompanyHorizontal name={company ? company.name : ""} description={company ? company.description : undefined} />
            <form
                style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "0.75rem" }}
            >
                <SearchSelect
                    handleSelect={(e) => onSelect(e)}
                    fetcher={getLocation}
                    placeholder="Ingresa la ciudad en la que se encuentra"
                    variant={SearchVariant.Basic}
                />
                <HStack>
                    <Input
                        name="street"
                        variant='filled'
                        type="text"
                        placeholder='Nombre de calle'
                        value={form.street}
                        onChange={(e) => handleChange(e)}
                        _focus={{ borderColor: "gray.100" }} />
                    <Input
                        name='streetNumber'
                        variant='filled'
                        type="number"
                        value={form.streetNumber}
                        onChange={(e) => handleChange(e)}
                        autoComplete="text"
                        placeholder='Numero de calle'
                        _focus={{ borderColor: "gray.100" }}
                        w="30%"
                    />
                </HStack>
                <HStack>
                    <Input
                        name='floor'
                        variant='filled'
                        type="text"
                        autoComplete="text"
                        placeholder='Piso'
                        value={form.floor}
                        onChange={(e) => handleChange(e)}
                        _focus={{ borderColor: "gray.100" }}
                        w="30%"
                    />
                    <Input
                        name='apartment'
                        variant='filled'
                        type="text"
                        autoComplete="text"
                        placeholder='Departamento'
                        onChange={(e) => handleChange(e)}
                        value={form.apartment}
                        _focus={{ borderColor: "gray.100" }}
                    />
                    <Input
                        name='postalCode'
                        variant='filled'
                        type="text"
                        autoComplete="text"
                        onChange={(e) => handleChange(e)}
                        value={form.postalCode}
                        placeholder='Codigo postal'
                        _focus={{ borderColor: "gray.100" }}
                    />
                </HStack>

            </form>
            <HStack justifyContent="end" w="100%">
                <Button
                    onClick={(e) => handleNext(e)}
                    _hover={{
                        bg: "black"
                    }}
                    _active={{
                        bg: "black"
                    }}
                    bg="brand.100"
                    color="white"
                >
                    {
                        isLoading ?
                        <Spinner/>
                        :
                        "Guardar"
                    }
                </Button>
            </HStack>
        </VStack >
    )
}

export default StepFive