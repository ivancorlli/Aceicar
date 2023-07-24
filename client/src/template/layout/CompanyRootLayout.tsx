'use client'
import { Outfit } from 'next/font/google'

// If loading a variable font, you don't need to specify the font weight
const font = Outfit({ subsets: ['latin'] })

import { Container, Grid, GridItem } from "@chakra-ui/react"
import React from "react"
import CompanySidebarShort from '@/component/Sidebar/CompanySidebarShort'
import { CompanySidebarLarge } from '@/component/Sidebar/CompanySidebarLarge'
import { useCompanyLogged } from '@/hook/myCompany'
import { redirect } from 'next/navigation'
import CompanyMobileNavbar from '@/component/Navbar/CompanyMobileNavbar'


function CompanyRootLayout({ children }: { children: React.ReactNode }) {
    const data = useCompanyLogged()
    if (data.redirect) {
        return redirect("/")
    }
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
                        <CompanyMobileNavbar />
                        <CompanySidebarShort />
                        <CompanySidebarLarge />
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
                    maxW={["98%","97%","94%%"]}
                    maxH="100%"
                    h="100%"
                    w="100%"
                    bg="white"
                    px={["10px","20px","30px","40px","50px"]}
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

export default CompanyRootLayout