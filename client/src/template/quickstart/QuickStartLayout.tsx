'use client'
import { Box, Container ,Step, StepDescription, StepIcon, StepIndicator, StepNumber, StepSeparator, StepStatus, StepTitle, Stepper, VStack, useSteps } from '@chakra-ui/react'
import { withPageAuthRequired } from "@auth0/nextjs-auth0/client";
import React, { createContext } from 'react'


const steps = [
  { title: 'Primero', description: 'Configurar cuenta' },
  { title: 'Segundo', description: 'Configurar perfil' },
  { title: 'Tercero', description: 'Seleccionar ubicacion' },
]

const QuickStartContext =   createContext((position:number)=>{})


const QuickStartLayout = ({ children }: { children: React.ReactNode }) => {
  const { activeStep, setActiveStep } = useSteps({
    index: 0,
    count: steps.length,
  })

  function Advance(position:number) {
    console.log("pos",position)
    if(position === 0 || position <= steps.length){
      setActiveStep(position)
    }
  }
  return (
    <>
    <QuickStartContext.Provider value={(num)=>Advance(num)}>
      <VStack h="100%" alignItems="center" spacing={2} py="2rem">
        <VStack w="100%" alignItems="center" justifyContent="center" alignSelf="start">
          <Guide index={activeStep} />
        </VStack >
        <Container py="3rem" w="100%" maxW="100%">
          {children}
        </Container>
      </VStack>
    </QuickStartContext.Provider>
    </>
  )
}



function Guide({ index }: { index: number }) {


  return (
    <Stepper size="lg" w="75%" index={index} colorScheme="blackAlpha">
      {steps.map((step, index) => (
        <Step key={index}>
          <StepIndicator>
            <StepStatus
              complete={<StepIcon />}
              incomplete={<StepNumber />}
              active={<StepNumber />}
            />
          </StepIndicator>

          <Box flexShrink='0'>
            <StepTitle>{step.title}</StepTitle>
            <StepDescription >{step.description}</StepDescription>
          </Box>

          <StepSeparator />
        </Step>
      ))}
    </Stepper>
  )

}





export {QuickStartContext}
export default withPageAuthRequired(QuickStartLayout)