'use client'
import { Outfit } from 'next/font/google'

// If loading a variable font, you don't need to specify the font weight
const font = Outfit({ subsets: ['latin'] })

import SidebarLarge from "@/component/Sidebar/SidebarLarge"
import { Container, Grid, GridItem } from "@chakra-ui/react"
import React from "react"
import MobileNavbar from '@/component/Navbar/MobileNavbar'
import SidebarShort from '@/component/Sidebar/SidebarShort'

function RootLayout({ children }: { children: React.ReactNode }) {
    return (
        <>
            <Container w='100%' p='0' maxW='100%' className={font.className} bg="brand.200">
                <Grid
                    w='100%'
                    h='100%'
                    templateColumns={['1fr', '65px 1fr', '65px 1fr', '200px 1fr']}
                    gap='0'
                >
                    <SideBars>
                        <MobileNavbar />
                        <SidebarShort />
                        <SidebarLarge />
                    </SideBars>
                    <Content >
                        {children}
                    </Content>
                </Grid>
            </Container>
        </>
    )
}
function Content({ children }: { children: React.ReactNode }) {
    return (
        <>
            <GridItem
                w="100%"
                h={['93vh', "100vh"]}
                py={["10px"]}
                className="content"
            >
                <Container
                    maxW={["98%", "97%", "94%%"]}
                    maxH="100%"
                    h="100%"
                    w="100%"
                    bg="white"
                    px={["10px", "20px", "30px", "40px", "50px"]}
                    shadow="lg"
                    borderRadius="2xl"
                >

                    {children}
                </Container>
            </GridItem>
        </>
    )
}


function SideBars({ children }: { children: React.ReactNode }) {
    return (
        <>
            <GridItem
                h={["45px", '100vh']}
                maxH='100vh'
                bg="brand.200"
                position="sticky"
                top="0"
                left="0"
            >
                {children}
            </GridItem>

        </>
    )
}

export default RootLayout