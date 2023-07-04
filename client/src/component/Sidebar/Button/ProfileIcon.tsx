import { Avatar, Box, Tooltip } from '@chakra-ui/react';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import React from 'react'

type Props = {
    src?: string,
    text?: string,
    link: string
}
const ProfileIcon = (props:Props) => {
    const params = usePathname();
    return (
        <Link href={props.link} style={{ width: "100%" }}>
            <Tooltip label={props.text ?? "Ver perfil"}>

                <Box
                    _hover={{ bg: "brand.100", color: "white", borderRadius: "md" }}
                    bg={params == props.link ? "brand.100" : "white"}
                    color={params == props.link ? "white" : "brand.100"}
                    borderRadius={params == props.link ? "md" : "none"}
                    padding="10px" 
                    textAlign="center"
                    >
                    <Avatar name={props.text ? props.src : ""} src={props.src ?? ""} size="sm" textAlign="center" />
                </Box>
            </Tooltip>
        </Link>
    )
}

export default ProfileIcon