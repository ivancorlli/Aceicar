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
            <Container w='100%' p='0' maxW={['100%', '100%', '100%', '100%', '100%', '90%']} className={font.className}>
                <Grid
                    w='100%'
                    h='100%'
                    templateColumns={['1fr', '75px 1fr', '75px 1fr', '200px 1fr']}
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
            <GridItem w="100%" h={['95vh',"100vh"]} px={["none","10xpx"]} className="content" bg="brand.200">
                {children}
            </GridItem>
        </>
    )
}


function SideBars({ children }: { children: React.ReactNode }) {
    return (
        <>
            <GridItem
                h={["45px",'100vh']}
                maxH='100vh'
                bg="white"
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