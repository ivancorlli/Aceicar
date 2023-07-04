import { Button, Container, Input, Select, VStack } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { ChangeEvent, FormEvent, useEffect, useState } from 'react'


interface IintialForm {
    country: string,
    city: string,
    state: string,
    postalCode: string
}

const intialForm: IintialForm = {
    country: "",
    city: "",
    state: "",
    postalCode: ""
}


const LocationConfiguration = () => {
    const [countries, setCountries] = useState([])
    const [form, setForm] = useState(intialForm)
    const router = useRouter()

    function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault()
        console.log(form)
        router.push("/quickstart?num=2")
    }

    function handleChange(e: ChangeEvent<HTMLInputElement>) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    }

    useEffect(() => {
        const fetchData = async () => {
            const data = await fetch('https://restcountries.com/v3.1/all');
            const json = await data.json();
            if (json.length > 0) {
                setCountries(json.map((pais: any) => pais.name.common))

            }
        }

        fetchData()
            .catch(console.error);
    }, [])

    return (
        <VStack w="100%">
            <Container w={["100%", "80%", "50%"]}>
                <form onSubmit={(e) => handleSubmit(e)} style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "2rem" }}>
                    <VStack w="100%">
                        <Select
                            placeholder='Seleccion un pais'
                            variant='filled'
                            autoCapitalize="true"
                            autoComplete="name"
                            borderColor="brand.100"
                            bg="white"
                            _focus={{ borderColor: "brand.100" }}
                        >
                            {
                                countries.map((pais, idx) => {
                                    return <option value={pais} key={pais}>{pais}</option>
                                })
                            }
                        </Select>
                    </VStack>
                    <Button type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
                        Continuar
                    </Button>
                </form>
            </Container>

        </VStack>
    )
}

export default LocationConfiguration