import { Button, VStack } from '@chakra-ui/react'
import Image from 'next/image'
import React, { useLayoutEffect } from 'react'
import CompanyLogin from "../../../../public/CompanyLogin.svg"
import { useRouter } from 'next/navigation'

interface INewCompany {
    created:boolean,
}

const Finished = ({newCompany}:{newCompany:INewCompany}) => {
    const router = useRouter()
    useLayoutEffect(()=>{
        if(!newCompany.created)
        {
           return router.push("/")
        }
    },[])
    return (
        <VStack w="100%" spacing={6}>
            <Image src={CompanyLogin} alt='Copany login' width={125} height={125} />
            <VStack w="100%">
                <Button
                    w="100%"
                    onClick={() => router.push("/")}
                    _hover={{
                        bg: "black"
                    }}
                    _active={{
                        bg: "black"
                    }}
                    bg="brand.100"
                    color="white"
                >
                    Regresar a inicio
                </Button>
            </VStack>
        </VStack>
    )
}

export default Finished