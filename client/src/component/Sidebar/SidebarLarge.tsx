'use client'

import { Button, Container, Divider, VStack, useColorMode } from '@chakra-ui/react'
import { signIn } from 'next-auth/react';
import React from 'react'

const SidebarLarge = () => {
  const {colorMode} = useColorMode();
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
            <Button onClick={()=>signIn("aceicar")} >Iniciar Sesion</Button>
          </VStack>
        }
      </Container>
    </VStack>
  )
}

export default SidebarLarge