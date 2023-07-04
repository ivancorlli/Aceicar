import { Container, Divider,  VStack, useColorMode } from '@chakra-ui/react'
import React from 'react'
import { usePathname } from 'next/navigation'
import { BiSolidHome } from 'react-icons/bi'
import { SlHome, SlLogin, SlLogout, SlSettings, SlSupport } from 'react-icons/sl'
import { IoCarSport, IoCarSportOutline, IoSettingsSharp, IoSpeedometerOutline, IoSpeedometerSharp } from 'react-icons/io5'
import { MdSupport } from 'react-icons/md'
import { PiShoppingBagFill, PiShoppingBagLight } from 'react-icons/pi'
import IconButton from './Button/IconButton'
import ProfileIcon from './Button/ProfileIcon'
import useUser from '@/hook/useUser'

const SidebarShort = () => {
    const { colorMode } = useColorMode();
    const { user,isLoading} = useUser();
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
            borderRight='1px'
            borderColor={colorMode == 'light' ? 'gray.100' : 'whiteAlpha.200'}
        >
            <Container w='100%' m="0" px="5px" >
                <VStack w='100%' spacing={2} alignItems='center' justifyContent="center">
                    {
                        isLoading
                            ?
                            ""
                            :
                            <>  
                            <IconButton icon={path === "/" ? BiSolidHome : SlHome} text='Inicio' link='/' />  
                                {

                                    user != null
                                        ?
                                        <>
                                            <IconButton icon={path === "/mycars" ? IoCarSport : IoCarSportOutline} text='Vehiculos' link='/mycars' />
                                            <IconButton icon={path === "/mybills" ? IoSpeedometerSharp : IoSpeedometerOutline} text='Resumen' link='/mybills' />
                                            <IconButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                            <IconButton icon={path === "/settings" ? IoSettingsSharp : SlSettings} text='Configuracion' link='/settings' />
                                            <IconButton icon={path === "/business" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/business' />
                                        </>
                                        :
                                        <>
                                            <IconButton icon={path === "/support" ? MdSupport : SlSupport} text='Soporte' link='/support' />
                                            <IconButton icon={path === "/business" ? PiShoppingBagFill : PiShoppingBagLight} text='Registra tu negocio' link='/business' />
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
                                ? ""
                                :
                                <>
                                    <Divider />
                                    {
                                        user != null
                                            ? <>
                                                <ProfileIcon text={user.Profile?.Name ?? undefined} src={user.ProfileImage ? user.ProfileImage : user.Email} link={`/user/${user.UserId}`} />
                                                <IconButton icon={SlLogout} text='Cerrar Sesion' link='/api/auth/logout' />
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