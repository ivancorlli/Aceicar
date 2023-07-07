'use client'
import SearchSelect, { SelectOptions } from '@/component/Input/SearchSelect'
import { Button, Container, VStack } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { ChangeEvent, FormEvent, useEffect, useState } from 'react'


async function fetcher(search:string):Promise<SelectOptions[]>
{
    const res = await fetch(`https://api.teleport.org/api/cities/?search=${search}`)
    const data = await res.json()
    let results:SelectOptions[] = []
    if(data != null)
    {
        if(data.count > 0)
        {
            const cities = data._embedded["city:search-results"]
            cities.map((e:any)=>{
                let option:SelectOptions = {
                    value: e._links["city:item"].href,
                    label: e["matching_full_name"]
                }
                results.push(option)
            })
        }
    }
    return results
}

const LocationConfiguration = async () => {
    const router = useRouter()
    

    return (
        <VStack w="100%">
            <Container w={["100%", "80%", "50%"]}>
                <form style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "2rem" }}>
                    <VStack w="100%">
                        <SearchSelect fetcher={(e)=>fetcher(e)}/>
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
