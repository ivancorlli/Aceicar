'use client'

import { Box, Container, HStack, Input, InputGroup, InputRightElement, List, ListItem, Text } from "@chakra-ui/react";
import { ChangeEvent, MouseEvent, FocusEvent, KeyboardEvent, useState, useEffect } from "react";
import { SlArrowUp, SlMagnifier, SlOptions } from "react-icons/sl";

export interface SelectOptions {
  value: string
  label: string
}

function SearchSelect(
  {
    fetcher,
    opts = []
  }
    :
    {
      opts?: SelectOptions[],
      fetcher?: (param: string) => Promise<SelectOptions[]>
    }
) {
  const [isLoading, setIsLoading] = useState(false)
  const [isOpen, setOpen] = useState(false)
  const [options, setOptions] = useState(opts)
  const [search, setSearch] = useState("")

  useEffect(() => {
    if (fetcher != null) {
      const func = async () => {
        // const s = await fetcher(search)
        const s: SelectOptions[] = [
          {
            value: 'value',
            label: 'label1'
          },
          {
            value: 'value2',
            label: 'label2'
          }
        ]
        if (s.length > 0) {
          setOptions(s)
        }
      }
      func()
    }
    if (opts != null) {
      // let resultSearch = options.filter(x => x.label.toLowerCase().trim() == e.target.value.toLowerCase().trim())
    }
    setIsLoading(false)
    setOpen(true)
  }, [fetcher, opts, search])

  function handleOpenClose(e: MouseEvent<HTMLDivElement>) {
    setOpen(!isOpen)
  }

  async function handleChange(e: ChangeEvent<HTMLInputElement>) {
    if (e.target.value.length > 3) {
      setIsLoading(true)
      setSearch(e.target.value)
    } else {
      setIsLoading(false)
    }
  }

  // function handleFocus(e:FocusEvent<HTMLInputElement>)
  // {
  //   if(options.length >0)
  //   {
  //     setOpen(true)
  //   }else {
  //     setOpen(false)
  //   }
  // }
  // function handleLeaves(e:MouseEvent<HTMLInputElement>)
  // {
  //   setOpen(false)
  // }
  // function handleOut(e:FocusEvent<HTMLInputElement>)
  // {
  //   setOpen(false)
  // }

  // function handleKey(e:KeyboardEvent<HTMLInputElement>){
  //   if(e.code == "Escape") 
  //   {
  //     setOpen(false)
  //   }
  // }

  // function handleClick(e:MouseEvent<HTMLInputElement>)
  // {
  //   if(options.length > 0)
  //   {
  //     setOpen(true)
  //   }else {
  //     setOpen(false)
  //   }
  // }

  return (
    <Container w="100%">
      <HStack>
        <InputGroup>
          <Input
            w="100%"
            placeholder='Nombre de usuario'
            borderColor="brand.100"
            bg="white"
            _focus={{ borderColor: "brand.100" }}
            variant='filled'
            type="text"
            onChange={(e) => handleChange(e)}
          // onFocus={(e)=>handleFocus(e)}
          // onMouseLeave={(e)=>handleLeaves(e)}
          // onBlur={(e)=>handleOut(e)}
          // onKeyUp={(e)=>handleKey(e)}
          // onClick={(e)=>handleClick(e)}
          />
          <InputRightElement bg="brand.100" onClick={(e) => handleOpenClose(e)} color="white" borderTopRightRadius={8} borderBottomRightRadius={8}>
            {
              isLoading
                ?
                <SlOptions />
                :
                isOpen ?
                  <SlArrowUp />
                  :
                  <SlMagnifier />
            }
          </InputRightElement>
        </InputGroup>
      </HStack>
      {

        isOpen ?
          <List
            bg="white"
            spacing={2}
            borderRadius={8}
            my="5px"
          >
            {
              options.length > 0 ?
                options.map((opt: SelectOptions, idx: number) => {
                  return (

                    <ListItem
                      p="5px"
                      _hover={{ bg: "gray" }}
                      borderRadius={8}
                      key={idx}
                    >
                      {opt.label}
                    </ListItem>)
                })
                :
                <Box w="100%" p="10px" textAlign="center">
                  <Text> Sin resultados </Text>
                </Box>
            }
          </List>
          :
          <>
          </>
      }
    </Container>
  )
}

// function ListBox({ options, isOpen }: { options: SelectOptions[], isOpen: boolean }) {
//   console.log(options)
//   return (
//     isOpen ?
//       <List
//         bg="white"
//         spacing={2}
//         borderRadius={8}
//         my="5px"
//       >
//         {
//           options.length > 0 ?
//             options.map((opt: SelectOptions, idx: number) => {
//               return (

//                 <ListItem
//                   p="5px"
//                   _hover={{ bg: "gray" }}
//                   borderRadius={8}
//                   key={idx}
//                 >
//                   {opt.label}
//                 </ListItem>)
//             })
//             :
//             <Box w="100%" p="10px" textAlign="center">
//               <Text> Sin resultados </Text>
//             </Box>
//         }
//       </List>
//       :
//       <>
//       </>
//   )

// }

export default SearchSelect