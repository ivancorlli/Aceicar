'use client'

import { Button, Container,Drawer, DrawerContent, DrawerOverlay, HStack, Icon, Text, VStack, useDisclosure } from '@chakra-ui/react'
import React from 'react'
import SidebarButton from '../Sidebar/Button/SidebarButton'
import ProfileButton from '../Sidebar/Button/ProfileButton'
import { usePathname } from 'next/navigation'
import { SlActionUndo, SlHome, SlLogin, SlLogout, SlSupport } from 'react-icons/sl'
import { IoCalendarOutline, IoCalendarSharp} from 'react-icons/io5'
import { MdSupport } from 'react-icons/md'
import { BiSolidHome } from 'react-icons/bi'
import { useAccount } from '@/hook/myAccount'
import Link from 'next/link'
import { useCompanyLogged } from '@/hook/myCompany'
import { BsBox, BsBoxFill } from 'react-icons/bs'

const CompanyMobileMenu = () => {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const path = usePathname();
    const { company, isLoading} = useCompanyLogged();
    const { user } = useAccount()
    return (
        <>
            <Button
                onClick={onOpen}>
                Open
            </Button>
            <Drawer
                isOpen={isOpen}
                placement='left'
                onClose={onClose}
                
            >
                <DrawerOverlay />
                <DrawerContent bg="brand.200">
                    <VStack
                        w='100%'
                        h='100%'
                        spacing={10}
                        paddingY='15px'
                        justifyContent='space-between'
                    >
                        <Container w='100%' m="0" px="5px" >
                            <VStack w='100%' spacing={2} alignItems='start'>
                                {
                                    isLoading
                                        ?
                                        ""
                                        :
                                        <>
                                        <SidebarButton icon={path === "/dashboard" ? BiSolidHome : SlHome} text="Inicio" link='/dashboard' />
                                        {
        
                                            company
                                                ?
                                                <>
                                                    <SidebarButton icon={path === "/dashboard/products" ? BsBoxFill : BsBox} text='Productos' link='/dashboard/products' />
                                                    <SidebarButton icon={path === "/dashboard/services" ? IoCalendarSharp : IoCalendarOutline} text='Servicios' link='/dashboard/services' />
                                                    <SidebarButton icon={path === "/dashboard/support" ? MdSupport : SlSupport} text='Soporte' link='/dashboard/support' />
                                                    {
                                                        user ?
                                                            <SidebarButton icon={SlActionUndo} text={`Regresar a ${user.profile && user.profile.name ? user.profile.name : "cuenta de usuario"}`} link='/api/access/logout' />
                                                            :
                                                            <>
                                                            </>
                                                    }
                                                </>
                                                :
                                                <>
                                                    <SidebarButton icon={path === "/dashboard/support" ? MdSupport : SlSupport} text='Soporte' link='/dahboard/support' />
                                                    {
                                                        user ?
                                                            <SidebarButton icon={SlActionUndo} text={`Regresar a ${user.profile && user.profile.name ? user.profile.name : "cuenta de usuario"}`} link='/api/access/logout' />
                                                            :
                                                            <>
                                                            </>
                                                    }
                                                </>
                                        }
                                    </>

                                }
                            </VStack>
                        </Container>
                        <Container w='100%' px="5px" m="0" >
                            {
                                <VStack w='100%' spacing={5} alignItems="start">
                                                                   {
                                    company
                                        ? <>
                                            <ProfileButton text={company.name} src={company.picture ? company.picture : ""} link={`/dashboard/profile/${company.companyId}`} />

                                            <Link href="/api/auth/logout" prefetch={false} style={{ width: "100%" }}>
                                                <HStack
                                                    _hover={{ bg: "brand.100", color: "white", borderRadius: "md" }}
                                                    bg={"brand.200"}
                                                    color={"brand.100"}
                                                    borderRadius={"none"}
                                                    padding="10px"
                                                    alignItems="center"
                                                >
                                                    <Icon as={SlLogout} w={5} h={5} />
                                                    <Text
                                                        fontWeight={"light"}
                                                        lineHeight="1"
                                                        letterSpacing="0"
                                                    >
                                                        Cerrar sesion
                                                    </Text>
                                                </HStack>
                                            </Link>
                                        </>
                                        :
                                        <SidebarButton icon={SlLogin} text='Iniciar Sesion' link='/api/auth/login' />
                                }
                                </VStack>
                            }
                        </Container>
                    </VStack>
                </DrawerContent>
            </Drawer>
        </>
    )
}

export default CompanyMobileMenu