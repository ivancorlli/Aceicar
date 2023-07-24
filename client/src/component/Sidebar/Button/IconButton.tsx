import { Box, Icon, Tooltip } from '@chakra-ui/react'
import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React from 'react'
import { IconType } from 'react-icons'

type Props = {
    icon: IconType,
    text: string,
    link: string
}


const IconButton = (props: Props) => {
    const params = usePathname();
    return (
        <>
            <Link href={props.link} style={{ width: "100%" }} >
                <Tooltip
                    label={props.text}
                >
                    <Box
                        _hover={{ bg: "brand.100", color: "white", borderRadius: "md" }}
                        bg={params == props.link ? "brand.100" : "brand.200"}
                        color={params == props.link ? "white" : "brand.100"}
                        borderRadius={params == props.link ? "md" : "none"}
                        padding="7px"
                        textAlign="center"
                    >
                        <Icon
                            as={props.icon}
                            boxSize={7}
                            textAlign="center"
                        />
                    </Box>
                </Tooltip>
            </Link>
        </>
    )
}

export default IconButton