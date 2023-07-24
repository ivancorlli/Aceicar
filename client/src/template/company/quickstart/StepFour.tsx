import CompanyHorizontal from '@/component/Card/CompanyHorizontal'
import { useAccount } from '@/hook/myAccount'
import { useCompanyLogged, usePostContact } from '@/hook/myCompany'
import { Button, HStack, Input, InputGroup, InputLeftAddon, Spinner, VStack, useToast } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { ChangeEvent, MouseEvent, useEffect, useState } from 'react'

const StepFour = () => {
    const router = useRouter()
    const { user } = useAccount()
    const {company} = useCompanyLogged()
    const [form, setForm] = useState<{ email: string, country: string, number: string, companyId: string }>({ email: user?.email.value ?? "", country: "ARG", number: user?.phone?.number ?? "", companyId: company?.companyId ?? "" });
    const { post, isLoading, isSuccess, isError,error } = usePostContact()
    const toast = useToast()


    useEffect(()=>{
        if(isSuccess)
        {
            toast({
                title: "Se guardaron los datos correctamente",
                status: "success",
                position: "top",
                isClosable: true,
            })
            router.push("/dashboard/quickstart?step=2")
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

    useEffect(()=>{
        setForm({
            ...form,
            email: company ? 
                company.contactData ? company.contactData.email : user?.email.value ?? "" 
                    : "",
            number : company ? 
                company.contactData ? company.contactData.number : user?.phone?.number ?? ""
                    : "",
            companyId: company?.companyId ?? ""
        })
    },[user,company])

    function handleChange(e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        setForm({
            ...form,
            [e.target.name]: e.target.value,
            companyId: company?.companyId ?? ""
        }
        )
    }

    async function Save(e: MouseEvent<HTMLButtonElement>) {
        e.preventDefault()
        post(form)
    }

    return (
        <VStack alignItems="center" w="100%" spacing={6}>
            <CompanyHorizontal name={company ? company.name : ""} description={company ? company.description : undefined} />
            <form
                style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "1rem" }}
            >
                <Input
                    name="email"
                    variant='filled'
                    type="text"
                    value={form.email}
                    placeholder='Email'
                    onChange={(e) => handleChange(e)}
                    _focus={{ borderColor: "gray.100" }} />
                <InputGroup>
                    <InputLeftAddon
                        children='+54'
                        bg="gray.300"
                        color="brand.100"
                        fontWeight="bold"
                    />
                    <Input
                        name='number'
                        variant='filled'
                        type="tel"
                        value={form.number}
                        onChange={(e) => handleChange(e)}
                        autoComplete="tel"
                        placeholder='Telefono'
                        _focus={{ borderColor: "gray.100" }}
                    />
                </InputGroup>
            </form>
            <HStack justifyContent="end" w="100%">
                <Button
                onClick={(e)=>Save(e)}
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
                            <Spinner />
                            :
                            "Guardar"
                    }
                </Button>
            </HStack>
        </VStack >
    )
}

export default StepFour