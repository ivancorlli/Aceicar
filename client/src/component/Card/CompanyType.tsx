'use client'
import { Card, CardBody, CardHeader, Heading, Icon } from '@chakra-ui/react'
import Image from 'next/image'
import React, { MouseEvent } from 'react'
import { SlAnchor } from 'react-icons/sl'


const CompanyType = ({ onSelect, selected, id, icon, name }: { onSelect: (id: string) => void, selected: string, id: string, icon?: string, name: string }) => {
    function handleSelect(e: MouseEvent<HTMLDivElement>) {
        onSelect(id)
    }
    return (
        <Card
            onClick={(e) => handleSelect(e)}
            _hover={{
                bg: selected == id ? "black" : "brand.100",
                color: "white",
                cursor: "pointer",
            }}
            fontSize="md"
            bg={selected == id ? "brand.100" : "white"}
            color={selected == id ? "white" : "brand.100"}
        >
            <CardHeader textAlign="center" w="100%">
                <Heading fontSize="inherit" fontWeight="medium"  > {name}</Heading>
            </CardHeader>
            <CardBody textAlign="center">
                {
                    icon ?
                        <Image src={icon} alt='logo' width={25} height={25} />
                        :
                        <Icon boxSize={10} as={SlAnchor} />
                }
            </CardBody>
        </Card>
    )
}

export default CompanyType