'use client'

import { SlHome, SlLogin, SlLogout, SlSettings, SlSupport } from 'react-icons/sl';
import { PiShoppingBagFill, PiShoppingBagLight } from 'react-icons/pi';
import { MdSupport } from 'react-icons/md'
import { IoCarSport, IoCarSportOutline, IoSettingsSharp, IoSpeedometerOutline, IoSpeedometerSharp } from 'react-icons/io5';
import { BiSolidHome } from 'react-icons/bi'
import { Container, Divider, VStack, useColorMode } from '@chakra-ui/react'
import { useUser } from '@auth0/nextjs-auth0/client';
import React from 'react'
import SidebarButton from './Button/SidebarButton';
import ProfileButton from './Button/ProfileButton';
import { usePathname } from 'next/navigation';

const SidebarLarge = () => {
  const { colorMode } = useColorMode();
  const { user: session, isLoading } = useUser();
  const path = usePathname();

  return (
    <VStack
      w='100%'
      h='100%'
      maxW='250px'
      spacing={10}
      paddingY='15px'
      justifyContent='space-between'
      borderRight='1px'
      borderColor={colorMode == 'light' ? 'gray.100' : 'whiteAlpha.200'}
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

                  session != null
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
                    session != null
                      ? <>
                        <ProfileButton text={session.name ?? ""} src={session.picture ?? ""} link={`/user/${session.org_id}`} />
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
  )
}

export default SidebarLarge