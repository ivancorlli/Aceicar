import { Avatar, HStack, Text } from '@chakra-ui/react'
import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React from 'react'

type Props = {
    src?: string,
    text?: string,
    link: string
}

const ProfileButton = (props: Props) => {
    const params = usePathname();
    return (
        <Link href={props.link} style={{ width: "100%" }}>
            <HStack
                _hover={{ bg: "brand.100", color: "white", borderRadius: "md" }}
                bg={params == props.link ? "brand.100" : "white"}
                color={params == props.link ? "white" : "brand.100"}
                borderRadius={params == props.link ? "md" : "none"}
                padding="10px" >
                <Avatar name={props.text == undefined ? props.src : ""} src={props.src ?? ""} size="sm" />
                <Text
                    fontWeight={"light"}
                    lineHeight="1"
                    letterSpacing="0"
                >
                    {props.text ?? "Ver perfil"}
                </Text>
            </HStack>
        </Link>
    )
}

export default ProfileButton