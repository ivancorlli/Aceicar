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
            _hover={ params == props.link ? { bg: "white", color: "black", borderRadius:"md"}:{ bg: "black", color: "white", borderRadius:"md"}}
            bg={params == props.link ? "black" : "white"}
            color={params == props.link ? "white":"black"}
            borderRadius={params == props.link ? "md" : "none"}
            padding="10px" >
                <Icon as={props.icon}w={5} h={5} />
                <Text>
                    {props.text}
                </Text>
            </HStack>
        </Link>
    )
}

export default SidebarButton