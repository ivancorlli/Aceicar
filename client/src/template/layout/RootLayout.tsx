'use client'
import SidebarLarge from "@/component/Sidebar/SidebarLarge"
import { Container, Grid, GridItem, Hide, Show } from "@chakra-ui/react"
import { useSession } from "next-auth/react"
import React from "react"



function RootLayout({ children }: { children: React.ReactNode }) {
    const { data: session, status } = useSession()
    console.log(session,status);
    return (
        <>
            <Container w='100%' p='0' maxW={['100%', '100%', '100%', '100%', '100%', '90%']}>
                <Grid
                    w='100%'
                    h='100%'
                    templateColumns={['1fr', '75px 1fr', '75px 1fr', '200px 1fr']}
                    gap='0'
                >
                    <SideBars>
                        <SidebarLarge/>
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
            <GridItem w="100%" h='100vh' p='0' className="content" bg="blackAlpha.50">
                {children}
            </GridItem>
        </>
    )
}


function SideBars({ children }: { children: React.ReactNode }) {
    return (
        <>
            <GridItem h='100vh' position='sticky' top={0} left={0} bg="white">
                {children}
            </GridItem>

        </>
    )
}

export default RootLayout