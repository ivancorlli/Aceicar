'use client'

import { Button, Container, Divider, Drawer, DrawerContent, DrawerOverlay, HStack, Icon, Text, VStack, useDisclosure } from '@chakra-ui/react'
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
import { useAccount } from '@/hook/myAccount'
import { useAccess } from '@/hook/userAccess'
import Link from 'next/link'

const MobileMenu = () => {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const path = usePathname();
    const { user: auth, isLoading } = useUser()
    const { user } = useAccount()
    const { access } = useAccess(user?.userId);
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
                                            <SidebarButton icon={path === "/" ? BiSolidHome : SlHome} text='Inicio' link='/' />
                                            {


                                                auth != undefined && user
                                                    ?
                                                    <>
                                                        <SidebarButton icon={path === "/mycars" ? IoCarSport : IoCarSportOutline} text='Vehiculos' link='/mycars' />
                                                        <SidebarButton icon={path === "/mybills" ? IoSpeedometerSharp : IoSpeedometerOutline} text='Resumen' link='/mybills' />
                                                        <SidebarButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                                        {
                                                            access != null && access.length > 0 ?
                                                                access.length == 1 ?
                                                                    <SidebarButton icon={PiShoppingBagLight} text={`Ingresa a ${access[0].company.name}`} link={`/api/access/login/${access[0].accessId}`} />
                                                                    :
                                                                    <SidebarButton icon={path === "/dashboard" ? PiShoppingBagFill : PiShoppingBagLight} text='Tus accesos' link='/dashboard' />
                                                                :
                                                                <SidebarButton icon={path === "/create-a-company" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/create-a-company' />
                                                        }
                                                    </>
                                                    :
                                                    <>
                                                        <SidebarButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                                        <SidebarButton icon={path === "/create-a-company" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/create-a-company' />
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
                                        auth != undefined && user
                                            ? <>
                                                <ProfileButton text={user.profile?.name && user.profile?.surname ? `${user.profile?.name} ${user.profile?.surname}` : undefined} src={user.picture ? user.picture : ""} link={`/user/${user.userId}`} />
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

export default MobileMenu