import { Avatar, Button, HStack, Input, Spinner, Textarea, VStack, useToast } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { ChangeEvent, MouseEvent, useEffect, useState } from 'react'


interface IForm {
    typeId?: string,
    specializationId?: string,
    name?: string,
    description?: string
}

const StepThree = (
    {
        form,
        onChange,
        onCreate,
        createStatus
    }:
        {
            form: IForm,
            onChange: (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void,
            onCreate: () => void,
            createStatus: { created: boolean, isLoading: boolean, error?: string }

        }) => {
    const toast = useToast()
    const router = useRouter()
    const [input ,setInput] = useState({name:"",description:""})

    useEffect(() => {
        if (!form.typeId) {
            router.push("/create-a-company")
        }
        if (!form.specializationId) {
            router.push("/create-a-company?step=2")
        }
    }, [])

    function handleNext(e: MouseEvent<HTMLButtonElement>) {
        if (form.name && form.name != "") {
            onCreate()
        } else {
            toast({
                title: "Debe ingresar el nombre del negocio",
                status: "error",
                position: "top",
                isClosable: true,
            })
        }
    }

    function handleChange(e:ChangeEvent<HTMLInputElement | HTMLTextAreaElement>)
    {
        setInput({
            ...input,
            [e.target.name]:e.target.value
        })
        onChange(e)
    }

    function handleBack(e: MouseEvent<HTMLButtonElement>) {
        router.push("/create-a-company?step=2")
    }
    return (
        <VStack alignItems="center" w="100%" spacing={6}>
            <Avatar src={""} name={""} bg="gray.200" size="xl" />
            <form
                style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "1rem" }}
            >
                <Input
                    name="name"
                    variant='filled'
                    type="text"
                    value={input.name}
                    placeholder='Nombre de negocio'
                    onChange={(e) => handleChange(e)}
                    _focus={{ borderColor: "gray.100" }} />

                <Textarea
                    name="description"
                    variant='filled'
                    autoComplete="text"
                    onChange={(e) => handleChange(e)}
                    value={input.description}
                    _focus={{ borderColor: "gray.100" }}
                    placeholder='Escriba una breve descpricion de lo que hace su negocio ...'
                />
            </form>
            <HStack justifyContent="space-between" w="100%">
                <Button
                    onClick={(e) => handleBack(e)}
                    _hover={{
                        bg: "black"
                    }}
                    _active={{
                        bg: "black"
                    }}
                    bg="brand.100"
                    color="white"
                >
                    Atras
                </Button>
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
                        createStatus.isLoading
                            ?
                            <>
                                <Spinner />

                            </>
                            :
                            "Crear"
                    }
                </Button>
            </HStack>
        </VStack>
    )
}

export default StepThree