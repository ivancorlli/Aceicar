import { HStack, PinInput, PinInputField } from '@chakra-ui/react'
import React from 'react'

type StepVerification = {
    code: string,
    handleChange: (e: string) => void
}

const EnterPin = (props:StepVerification) => {
    const {code,handleChange} = props
    return (
        <HStack>
            <PinInput otp size="lg" onChange={(e)=>handleChange(e)}>
                <PinInputField borderColor="brand.100" borderWidth="2px" _focusVisible={{borderColor:"brand.100"}}/>
                <PinInputField borderColor="brand.100" borderWidth="2px" _focusVisible={{borderColor:"brand.100"}}/>
                <PinInputField borderColor="brand.100" borderWidth="2px" _focusVisible={{borderColor:"brand.100"}}/>
                <PinInputField borderColor="brand.100" borderWidth="2px" _focusVisible={{borderColor:"brand.100"}}/>
                <PinInputField borderColor="brand.100" borderWidth="2px" _focusVisible={{borderColor:"brand.100"}}/>
                <PinInputField borderColor="brand.100" borderWidth="2px" _focusVisible={{borderColor:"brand.100"}}/>
            </PinInput>
        </HStack>
    )
}

export default EnterPin