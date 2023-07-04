'use client'
import EnterPin from '@/component/Input/EnterPin'
import { Button, Container, Heading, Text, VStack } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { FormEvent, useState } from 'react'

const PhoneVerification = () => {
    const [code, setCode] = useState("")
    const router = useRouter()

    function handleChange(e: string) {
        setCode(e)
        if (e.length === 6) {
            handleForm()
        }
    }

    function handleForm() {
        router.push("/quickstart?num=1")
    }

    function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault()
        handleForm()
    }


    return (
        <VStack spacing={5} w="100%" alignItems="center" justifyContent="center">
            <VStack>
                <Heading as="h1" size="lg">
                    Verificar telefono
                </Heading>
                <Text fontSize="sm" color="gray" w="70%" alignItems="center" textAlign="center">
                    Hemos enviado un codigo de verificacion a tu numero de telefono.
                </Text>
            </VStack>
            <Container w={["100%","80%"]}>
                <form onSubmit={(e) => handleSubmit(e)} style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "2rem" }}>

                    <EnterPin code='' handleChange={handleChange} />
                    <Button type="submit" bg="brand.100" color="white" variant='solid' w={["100%", "100%","50%"]} _hover={{ bg: "black" }}>
                        Continuar
                    </Button>
                </form>
            </Container>
        </VStack>
    )
}

export default PhoneVerification