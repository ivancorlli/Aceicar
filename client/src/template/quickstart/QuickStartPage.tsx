'use client'
import UserAccount from '@/component/Card/UserAccount'
import { Button, FormControl, Input, InputGroup, InputLeftAddon, VStack } from '@chakra-ui/react'
import React, { ChangeEvent, FormEvent, useState } from 'react'
import { useRouter, useSearchParams } from 'next/navigation'

interface IintialForm {
    username: string,
    phoneNumber: string
}
const intialForm: IintialForm = {
    username: "",
    phoneNumber: ""
}

export const QuickStartPage = () => {
    const [form, setForm] = useState(intialForm);
    const router = useRouter()
    const params = useSearchParams()?.get("num")
    function handleInputChange(e: ChangeEvent<HTMLInputElement>) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });

    }

    function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault()
        router.push("/quickstart?num=1")
        console.log("submit")
        console.log(params)

    }

    return (
        <VStack spacing={8} w="100%" alignItems="center" justifyContent="center">
            <UserAccount form={form} />
            <form onSubmit={(e) => handleSubmit(e)} style={{ width: "35%", display: "flex", flexDirection: "column", alignItems:"center",justifyContent:"center",gap:"2rem" }}>
                <VStack w="100%">
                    <Input name='username' defaultValue={form.username} onChange={(e) => handleInputChange(e)} variant='filled' type="text" autoCapitalize="true" autoComplete="text" placeholder='Nombre de usuario' bg="white" />
                    <InputGroup>
                        <InputLeftAddon children='+54' />
                        <Input name='phoneNumber' defaultValue={form.phoneNumber} onChange={(e) => handleInputChange(e)} variant='filled' type="tel" autoComplete="tel" placeholder='Telefono' bg="white" />
                    </InputGroup>
                </VStack>
                <Button type="submit"  bg="black" color="white" variant='solid' w="50%">
                    Continuar
                </Button>
            </form>
        </VStack>
    )
}
