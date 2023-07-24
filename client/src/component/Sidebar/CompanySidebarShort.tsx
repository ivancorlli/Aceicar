import { Box, Container, Divider, Icon, Skeleton, Tooltip, VStack, useColorMode } from '@chakra-ui/react'
import React from 'react'
import { usePathname } from 'next/navigation'
import { BiSolidHome } from 'react-icons/bi'
import { SlActionUndo, SlHome, SlLogin, SlLogout, SlSettings, SlSupport } from 'react-icons/sl'
import { IoCalendarOutline, IoCalendarSharp, IoCarSport, IoCarSportOutline, IoSettingsSharp, IoSpeedometerOutline, IoSpeedometerSharp } from 'react-icons/io5'
import { BsBox, BsBoxFill } from 'react-icons/bs'
import { MdSupport } from 'react-icons/md'
import { PiShoppingBagFill, PiShoppingBagLight } from 'react-icons/pi'
import IconButton from './Button/IconButton'
import ProfileIcon from './Button/ProfileIcon'
import { useUser } from '@auth0/nextjs-auth0/client'
import { useAccount } from '@/hook/myAccount'
import Link from 'next/link'
import { useCompanyLogged } from '@/hook/myCompany'

const CompanySidebarShort = () => {
    const { user } = useAccount()
    const { company, isLoading } = useCompanyLogged()
    const path = usePathname();
    return (
        <VStack
            display={["none", "flex", "flex", "none"]}
            w='100%'
            h='100%'
            maxW='75px'
            spacing={10}
            paddingY='15px'
            justifyContent="space-between"
        >
            <Container w='100%' m="0" px="5px" >
                <VStack w='100%' spacing={2} alignItems='center' justifyContent="center">
                    {
                        isLoading
                            ?
                            <>
                                <Skeleton w="100%" h="35px" p="8px" borderRadius="8px">
                                    <div>contents wrapped</div>
                                </Skeleton>
                                <Skeleton w="100%" h="35px" p="8px" borderRadius="8px">
                                    <div>contents wrapped</div>
                                </Skeleton>
                                <Skeleton w="100%" h="35px" p="8px" borderRadius="8px">
                                    <div>contents wrapped</div>
                                </Skeleton>
                            </>
                            :
                            <>
                                <IconButton icon={path === "/dashboard" ? BiSolidHome : SlHome} text='Inicio' link='/dashboard' />
                                {

                                    company
                                        ?
                                        <>
                                            <IconButton icon={path === "/dashboard/products" ? BsBoxFill : BsBox} text='Productos' link='/dashboard/products' />
                                            <IconButton icon={path === "/dashboard/services" ? IoCalendarSharp : IoCalendarOutline} text='Services' link='/dashboard/services' />
                                            <IconButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                            {
                                                user ?
                                                    <IconButton icon={SlActionUndo} text={`Regresar a ${user.profile && user.profile.name ? user.profile.name : "cuenta de usuario"}`} link='/api/access/logout' />
                                                    :
                                                    <></>
                                            }
                                        </>
                                        :
                                        <>
                                            <IconButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                            {
                                                user ?
                                                    <IconButton icon={SlActionUndo} text={`Regresar a ${user.profile && user.profile.name ? user.profile.name : "cuenta de usuario"}`} link='/api/access/logout' />
                                                    :
                                                    <></>
                                            }
                                        </>
                                }
                            </>

                    }
                </VStack>
            </Container>
            <Container w='100%' px="5px" m="0"  >
                {
                    <VStack w='100%' spacing={5} alignItems="center">
                        {
                            isLoading
                                ?
                                <>
                                    <Skeleton w="100%" h="35px" p="8px" borderRadius="8px">
                                        <div>contents wrapped</div>
                                    </Skeleton>
                                    <Skeleton w="100%" h="35px" p="8px" borderRadius="8px">
                                        <div>contents wrapped</div>
                                    </Skeleton>
                                </>
                                :
                                <>
                                    <Divider />
                                    {
                                        company
                                            ? <>
                                                <ProfileIcon text={company.name} src={company.picture ? company.picture : ""} link={`/dashboard/profile/${company.companyId}`} />
                                                <Link href={"/api/auth/logout"} prefetch={false} style={{ width: "100%" }} >
                                                    <Tooltip
                                                        label={"Cerrar sesion"}
                                                    >
                                                        <Box
                                                            _hover={{ bg: "brand.100", color: "white", borderRadius: "md" }}
                                                            bg={"brand.200"}
                                                            color={"brand.100"}
                                                            borderRadius={"none"}
                                                            padding="7px"
                                                            textAlign="center"
                                                        >
                                                            <Icon
                                                                as={SlLogout}
                                                                boxSize={7}
                                                                textAlign="center"
                                                            />
                                                        </Box>
                                                    </Tooltip>
                                                </Link>
                                            </>
                                            :
                                            <IconButton icon={SlLogin} text='Iniciar Sesion' link='/api/auth/login' />
                                    }
                                </>
                        }
                    </VStack>
                }
            </Container>
        </VStack>
    )
}

export default CompanySidebarShort