'use client'

import { Link } from '@chakra-ui/next-js';
import { Button, Container, Divider, VStack, useColorMode } from '@chakra-ui/react'
import { useUser } from '@auth0/nextjs-auth0/client';
import React from 'react'

const SidebarLarge = () => {
  const {colorMode} = useColorMode();
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
      <Container w='100%' >
        <VStack w='100%' spacing={5} alignItems='start' paddingX='10px' >
        </VStack>
      </Container>
      <Container w='100%' >
        {
          <VStack w='100%' spacing={5}>
            <Divider />
            <Link href="/api/auth/login">
              Iniciar Sesion
            </Link>
          </VStack>
        }
      </Container>
    </VStack>
  )
}

export default SidebarLarge