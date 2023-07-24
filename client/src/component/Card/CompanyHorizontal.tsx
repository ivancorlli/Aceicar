import Capitalize from '@/utils/Capitalize'
import { HStack, Image, VStack, Text } from '@chakra-ui/react'
import React from 'react'

const CompanyHorizontal = ({name,description}:{name:string,description?:string}) => {
    return (
        <HStack
            w="100%"
            justifyContent="start"
            spacing={4}
        >
            <Image
                w="75px"
                fallbackSrc='https://via.placeholder.com/150'
                borderRadius="lg"
            />
            <VStack spacing={0} alignItems="start">
                <Text fontSize="lg" fontWeight="semibold" >{Capitalize(name)}</Text>
                <Text fontSize="sm" color="gray">{description ?? "Aun no cargaste una descripcion del negocio"}</Text>
            </VStack>
        </HStack>
    )
}

export default CompanyHorizontal