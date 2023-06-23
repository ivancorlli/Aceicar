'use client'

import { SlHome, SlLogin } from 'react-icons/sl';
import {BiSolidHome} from 'react-icons/bi'
import {  Container, Divider, VStack, useColorMode } from '@chakra-ui/react'
import { useUser } from '@auth0/nextjs-auth0/client';
import React from 'react'
import SidebarButton from './Button/SidebarButton';

const SidebarLarge = () => {
  const { colorMode } = useColorMode();
  const { user, error, isLoading } = useUser();
  console.log(user)
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
        <VStack w='100%' spacing={5} alignItems='start'>
        <SidebarButton icon={BiSolidHome} text='Inicio' link='/' />
        </VStack>
      </Container>
      <Container w='100%' px="5px" m="0" >
        {
          <VStack w='100%' spacing={5} alignItems="start">
            <Divider />
            <SidebarButton icon={SlLogin} text='Iniciar Sesion' link='/api/auth/login' />
          </VStack>
        }
      </Container>
    </VStack>
  )
}

export default SidebarLarge