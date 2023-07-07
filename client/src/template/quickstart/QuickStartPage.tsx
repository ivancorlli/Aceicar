'use client'

import UserAccount from "@/component/Card/UserAccount";
import { Button, Container, Input, InputGroup, InputLeftAddon, VStack } from "@chakra-ui/react";
import { useSearchParams, useRouter } from "next/navigation";
import { ChangeEvent, FormEvent, useContext, useEffect, useLayoutEffect, useState } from "react";
import { QuickStartContext } from "./QuickStartLayout";
import PhoneVerification from "./PhoneVerification";
import ProfileConfiguration from "./ProfileConfiguration";
import LocationConfiguration from "./LocationConfiguration";


function QuickStartPage() {
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
            return <SetpOne />
        case "0":
            return <PhoneVerification />
        case "1":
            return <ProfileConfiguration />
        case "2":
            return <LocationConfiguration />
        case "3":
            return <>Configuracion Terminada</>
        default:
            break;

    }

}



interface IintialForm {
    username: string,
    phoneNumber: string
}
const intialForm: IintialForm = {
    username: "",
    phoneNumber: ""
}
function SetpOne() {
    const [form, setForm] = useState(intialForm);
    const router = useRouter()

    function handleChange(e: ChangeEvent<HTMLInputElement>) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    }

    function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault()
        router.push("/quickstart?num=0")
    }

    return (
        <>
            <VStack w="100%" >
                <UserAccount form={form} />
                <Container w={["100%","80%","40%"]}>
                    <form onSubmit={(e) => handleSubmit(e)} style={{ width: "100%%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "2rem" }}>
                        <VStack w="100%">
                            <Input name='username' defaultValue={form.username} onChange={(e) => handleChange(e)} variant='filled' type="text" autoCapitalize="true" autoComplete="text" placeholder='Nombre de usuario' borderColor="brand.100" bg="white" _focus={{ borderColor: "brand.100" }} />
                            <InputGroup>
                                <InputLeftAddon children='+54' bg="brand.100" color="white" borderColor="brand.100" />
                                <Input name='phoneNumber' defaultValue={form.phoneNumber} onChange={(e) => handleChange(e)} variant='filled' type="tel" autoComplete="tel" placeholder='Telefono' borderColor="#brand.100" _focus={{ borderColor: "brand.100" }} bg="white" />
                            </InputGroup>
                        </VStack>
                        <Button type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
                            Continuar
                        </Button>
                    </form>
                </Container>

            </VStack>
        </>
    )
}
export default QuickStartPage