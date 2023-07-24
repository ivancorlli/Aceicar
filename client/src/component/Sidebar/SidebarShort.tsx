import { Box, Container, Divider, Icon, Skeleton, Tooltip, VStack, useColorMode } from '@chakra-ui/react'
import React from 'react'
import { usePathname } from 'next/navigation'
import { BiSolidHome } from 'react-icons/bi'
import { SlActionUndo, SlHome, SlLogin, SlLogout, SlSettings, SlSupport } from 'react-icons/sl'
import { IoCarSport, IoCarSportOutline, IoSettingsSharp, IoSpeedometerOutline, IoSpeedometerSharp } from 'react-icons/io5'
import { MdSupport } from 'react-icons/md'
import { PiShoppingBagFill, PiShoppingBagLight } from 'react-icons/pi'
import IconButton from './Button/IconButton'
import ProfileIcon from './Button/ProfileIcon'
import { useUser } from '@auth0/nextjs-auth0/client'
import { useAccount } from '@/hook/myAccount'
import Link from 'next/link'
import { useAccess } from '@/hook/userAccess'

const SidebarShort = () => {
    const { user: auth, isLoading } = useUser();
    const { user } = useAccount()
    const { access } = useAccess(user?.userId);
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
                                <IconButton icon={path === "/" ? BiSolidHome : SlHome} text='Inicio' link='/' />
                                {

                                    auth != undefined && user
                                        ?
                                        <>
                                            <IconButton icon={path === "/mycars" ? IoCarSport : IoCarSportOutline} text='Vehiculos' link='/mycars' />
                                            <IconButton icon={path === "/mybills" ? IoSpeedometerSharp : IoSpeedometerOutline} text='Resumen' link='/mybills' />
                                            <IconButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                            {
                                                access != null && access.length > 0 ?
                                                    access.length == 1 ?
                                                        <IconButton icon={PiShoppingBagLight} text={`Ingresa a ${access[0].company.name}`} link={`/api/access/login/${access[0].accessId}`} />
                                                        :
                                                        <IconButton icon={path === "/dashboard" ? PiShoppingBagFill : PiShoppingBagLight} text='Tus accesos' link='/dashboard' />
                                                    :
                                                    <IconButton icon={path === "/create-a-company" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/create-a-company' />
                                            }
                                        </>
                                        :
                                        <>
                                            <IconButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                            <IconButton icon={path === "/create-a-company" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/create-a-company' />
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
                                        auth != undefined && user
                                            ? <>
                                                <ProfileIcon text={user.profile?.name && user.profile?.surname ? `${user.profile?.name} ${user.profile?.surname}` : undefined} src={user.picture ? user.picture : ""} link={`/user/${user.userId}`} />
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
export default SidebarShort