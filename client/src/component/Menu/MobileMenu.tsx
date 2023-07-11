'use client'

import { Button, Container, Divider, Drawer, DrawerContent, DrawerOverlay, VStack, useDisclosure } from '@chakra-ui/react'
import React from 'react'
import SidebarButton from '../Sidebar/Button/SidebarButton'
import ProfileButton from '../Sidebar/Button/ProfileButton'
import { usePathname } from 'next/navigation'
import { SlHome, SlLogin, SlLogout, SlSettings, SlSupport } from 'react-icons/sl'
import { IoCarSport, IoCarSportOutline, IoSettingsSharp, IoSpeedometerOutline, IoSpeedometerSharp } from 'react-icons/io5'
import { MdSupport } from 'react-icons/md'
import { PiShoppingBagFill, PiShoppingBagLight } from 'react-icons/pi'
import { BiSolidHome } from 'react-icons/bi'
import { useUser } from '@auth0/nextjs-auth0/client'
import {useAccount} from '@/hook/myAccount'

const MobileMenu = () => {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const path = usePathname();
    const { user:auth, isLoading } = useUser()
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
                <DrawerContent>
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
                                            <SidebarButton icon={path === "/" ? BiSolidHome : SlHome} text='Inicio' link='/' />
                                            {

                                                auth != undefined && user 
                                                    ?
                                                    <>
                                                        <SidebarButton icon={path === "/mycars" ? IoCarSport : IoCarSportOutline} text='Vehiculos' link='/mycars' />
                                                        <SidebarButton icon={path === "/mybills" ? IoSpeedometerSharp : IoSpeedometerOutline} text='Resumen' link='/mybills' />
                                                        <SidebarButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                                        <SidebarButton icon={path === "/settings" ? IoSettingsSharp : SlSettings} text='Configuracion' link='/settings' />
                                                        <SidebarButton icon={path === "/business" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/business' />
                                                    </>
                                                    :
                                                    <>
                                                        <SidebarButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                                        <SidebarButton icon={path === "/business" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/business' />
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
                                        isLoading
                                            ? ""
                                            :
                                            <>
                                                <Divider />
                                                {
                                                    auth != undefined && user 
                                                        ? <>
                                                            <ProfileButton text={user.Profile?.Name && user.Profile?.Surname ? `${user.Profile?.Name} ${user.Profile?.Surname}` : undefined} src={user.Picture ? user.Picture : user.Email} link={`/user/${user.UserId}`} />
                                                            <SidebarButton icon={SlLogout} text='Cerrar Sesion' link='/api/auth/logout' />
                                                        </>
                                                        :
                                                        <SidebarButton icon={SlLogin} text='Iniciar Sesion' link='/api/auth/login' />
                                                }
                                            </>
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

export default MobileMenu