import { HStack, Icon, Text } from '@chakra-ui/react'
import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React from 'react'
import { IconType } from 'react-icons'

type Props = {
    icon: IconType,
    text: string,
    link: string
}

const SidebarButton = (props: Props) => {
    const params = usePathname();
    return (
        <Link href={props.link} style={{ width: "100%"}}>
            <HStack 
            _hover={{ bg: "brand.100", color: "white", borderRadius:"md"}}
            bg={params == props.link ? "brand.100" : "white"}
            color={params == props.link ? "white":"brand.100"}
            borderRadius={params == props.link ? "md" : "none"}
            padding="10px" 
            alignItems="center"
            >
                <Icon as={props.icon}w={5} h={5}/>
                <Text 
                fontWeight={"light"}
                lineHeight="1"
                letterSpacing="0"
                >
                    {props.text}
                </Text>
            </HStack>
        </Link>
    )
}

export default SidebarButton