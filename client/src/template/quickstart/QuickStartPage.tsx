'use client'

import UserAccount from "@/component/Card/UserAccount";
import { Button, Container, HStack, Heading, Input, InputGroup, InputLeftAddon, VStack, useToast } from "@chakra-ui/react";
import { useSearchParams, useRouter, redirect } from "next/navigation";
import { ChangeEvent, FormEvent, useContext, useEffect, useState } from "react";
import { QuickStartContext } from "./QuickStartLayout";
import PhoneVerification from "./PhoneVerification";
import ProfileConfiguration from "./ProfileConfiguration";
import SearchSelect, { SelectOptions } from "@/component/Input/SearchSelect";
import { withPageAuthRequired } from '@auth0/nextjs-auth0/client'
import { useAccount, usePostAccount, usePostLocation } from "@/hook/myAccount";
import IUser from "@/lib/interface/IMyAccount";
import ProfileCompletedPic from "../../../public/ProfileCompleted.png"
import Image from "next/image";

async function fetcher(search: string): Promise<SelectOptions[]> {
    const res = await fetch(`https://api.teleport.org/api/cities/?search=${search}`)
    const data = await res.json()
    let results: SelectOptions[] = []
    if (data != null) {
        if (data.count > 0) {
            const cities = data._embedded["city:search-results"]
            cities.map((e: any) => {
                let option: SelectOptions = {
                    value: e._links["city:item"].href,
                    label: e["matching_full_name"]
                }
                results.push(option)
            })
        }
    }
    return results
}

function QuickStartPage() {
    const { user } = useAccount()
    const params = useSearchParams()?.get("num")
    const Advance = useContext(QuickStartContext)

    useEffect(() => {
        getPosition()
    }, [params])

    function getPosition(): void {
        switch (params) {
            case null:
                Advance(0)
                break;
            case "1":
                Advance(1)
                break;
            case "2":
                Advance(2)
                break;
            case "3":
                Advance(3)
                break;
            default:
                Advance(0)
        }
    }

    switch (params) {
        case null:
            return <SetpOne user={user} />
        case "0":
            return <PhoneVerification user={user} />
        case "1":
            return <ProfileConfiguration user={user} />
        case "2":
            return <LocationConfiguration user={user} />
        case "3":
            return <ProfileCompleted />
        default:
            break;

    }

}



function SetpOne({ user }: { user: IUser | null }) {
    const toast = useToast()
    const { post, isLoading } = usePostAccount()
    if (user != null) {
        if (user.username != undefined && user.phone != undefined) {
            if (user.phone.verified) {
                return redirect("/quickstart?num=1")
            } else {
                return redirect("/quickstart?num=0")
            }
        }
    }
    interface SetpOneForm {
        username: string,
        phoneCountry: string,
        phoneNumber: string,
        userId: string
    }
    const [form, setForm] = useState<SetpOneForm>({ username: user?.username ?? "", phoneCountry: "ARG", phoneNumber: user?.phone?.number ?? "", userId: user?.userId ?? "" });
    const router = useRouter()

    function handleChange(e: ChangeEvent<HTMLInputElement>) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    }

    function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault()
        if (validateSubmit()) {

            post(form)
            router.push("/quickstart?num=0")
        } else {
            toast({
                title: "Debe completar los datos de la cuenta",
                status: "error",
                position: "top",
                isClosable: true,
            })
        }
    }
    function validateSubmit(): boolean {
        let isValid: boolean = false
        if
            (
            form.username != "" &&
            form.userId != "" &&
            form.phoneCountry != "" &&
            form.phoneNumber != ""
        ) {
            isValid = true
        }
        return isValid;
    }

    return (
        <>
            <VStack w="100%" spacing={8}>
                <Heading size="lg">
                    Completa tu cuenta
                </Heading>
                <VStack w="100%">
                    <UserAccount form={{ username: form.username, phoneNumber: form.phoneNumber }} image={user?.picture} />
                    <Container w={["100%", "80%", "40%"]}>
                        <form onSubmit={(e) => handleSubmit(e)} style={{ width: "100%%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "2rem" }}>
                            <VStack w="100%">
                                <Input name='username' defaultValue={form.username} onChange={(e) => handleChange(e)} variant='filled' type="text" autoCapitalize="true" autoComplete="text" placeholder='Nombre de usuario' borderColor="brand.100" bg="white" _focus={{ borderColor: "brand.100" }} />
                                <InputGroup>
                                    <InputLeftAddon children='+54' bg="brand.100" color="white" borderColor="brand.100" />
                                    <Input name='phoneNumber' defaultValue={form.phoneNumber} onChange={(e) => handleChange(e)} variant='filled' type="tel" autoComplete="tel" placeholder='Telefono' borderColor="#brand.100" _focus={{ borderColor: "brand.100" }} bg="white" />
                                </InputGroup>
                            </VStack>
                            <Button type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
                                {
                                    isLoading ? "Enviando..." : "Continuar"
                                }
                            </Button>
                        </form>
                    </Container>
                </VStack>

            </VStack>
        </>
    )
}


function LocationConfiguration({ user }: { user: IUser | null }) {
    const toast = useToast()
    if (user != null) {
        if (user.location != undefined) {
            return redirect("/")
        }
    }
    interface LocationForm {
        country: string,
        city: string,
        state: string,
        postalCode: string,
        street: string,
        streetNumber: string,
        userId: string
    }
    const router = useRouter()
    const [form, setForm] = useState<LocationForm>({ country: "", city: "", state: "", postalCode: "", street: "", streetNumber: "", userId: user?.userId ?? "" });
    const { post, isLoading } = usePostLocation()

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

    function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault()
        post(form)
        router.push("/quickstart?num=3")
    }

    return (
        <VStack w="100%">
            <Container w={["100%", "80%", "50%"]}>
                <form onSubmit={(e) => handleSubmit(e)} style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "2rem" }}>
                    <VStack w="100%" spacing={5}>
                        <Heading size="lg">
                            Ingresa tu ubicacion
                        </Heading>
                        <VStack>
                            <SearchSelect fetcher={fetcher} handleSelect={(e) => onSelect(e)} placeholder="Ciudad en la que vives" />
                            <Input name='street' defaultValue={form.street} onChange={(e) => handleChange(e)} variant='filled' type="text" autoCapitalize="true" autoComplete="text" placeholder='Nombre de calle' borderColor="brand.100" bg="white" _focus={{ borderColor: "brand.100" }} />
                            <HStack>
                                <Input name='streetNumber' defaultValue={form.streetNumber} onChange={(e) => handleChange(e)} variant='filled' type="number" autoCapitalize="true" autoComplete="text" placeholder='Numero de calle' borderColor="brand.100" bg="white" _focus={{ borderColor: "brand.100" }} />
                                <Input name='postalCode' defaultValue={form.postalCode} onChange={(e) => handleChange(e)} variant='filled' type="text" autoCapitalize="true" autoComplete="text" placeholder='Codigo postal' borderColor="brand.100" bg="white" _focus={{ borderColor: "brand.100" }} />
                            </HStack>
                        </VStack>
                    </VStack>
                    <Button type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
                        {
                            isLoading ? "Enviando.." : "Continuar"
                        }
                    </Button>
                </form>
            </Container>

        </VStack >
    )
}


function ProfileCompleted() {
    const { push } = useRouter()
    return (
        <>
            <VStack w="100%">
                <Image src={ProfileCompletedPic} alt='Profile Completed' width={200} height={200} />
                <Button onClick={() => push("/")} type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
                    Volver a inicio
                </Button>
                <Button onClick={() => push("/vehicles")} type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
                    Registrar vehiculo
                </Button>
            </VStack>
        </>
    )
}


export default withPageAuthRequired(QuickStartPage)