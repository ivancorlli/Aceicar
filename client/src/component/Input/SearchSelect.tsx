'use client'

import { Box, Container, Fade, HStack, Input, InputGroup, InputRightElement, List, ListItem, Text, useDisclosure } from "@chakra-ui/react";
import { ChangeEvent, MouseEvent, FocusEvent, KeyboardEvent, useState, useEffect } from "react";
import { IoCloseOutline } from "react-icons/io5";
import { SlArrowUp, SlMagnifier, SlOptions } from "react-icons/sl";

export interface SelectOptions {
  value: string
  label: string
}

export enum SearchVariant {
  Outlined,
  Basic
}

function SearchSelect(
  {
    fetcher,
    opts = [],
    handleSelect,
    placeholder,
    variant = SearchVariant.Outlined
  }
    :
    {
      opts?: SelectOptions[],
      fetcher?: (param: string) => Promise<SelectOptions[]>,
      handleSelect: (option: SelectOptions) => Promise<void>,
      placeholder: string,
      variant?: SearchVariant
    }
) {
  const [isLoading, setIsLoading] = useState(false)
  const [options, setOptions] = useState<SelectOptions[]>(opts)
  const [search, setSearch] = useState("")
  const [value, setValue] = useState("")
  const [selectd, setSelected] = useState<SelectOptions | null>(null)
  const { isOpen, onToggle, onOpen, onClose } = useDisclosure()


  useEffect(() => {
    let ignore = false
    if (fetcher != undefined) {
      if (search != "" && search.length > 3) {
        fetcher(search).then((data: SelectOptions[]) => {
          if (!ignore) {
            setOptions(data)
            setIsLoading(false)
            onOpen()
          }
        })
      } else {
        if (selectd != null) {
          setValue(selectd.label)
          handleSelect(selectd)
        }
        setOptions([])
        onClose()
      }
    }
    return () => {
      ignore = true
    }

  }, [search, fetcher])

  function handleOpenClose(e: MouseEvent<HTMLDivElement>) {
    if(selectd!=null)
    {
      setSelected(null)
      setValue("")
    }else{
      onToggle()
    }
  }

  async function handleChange(e: ChangeEvent<HTMLInputElement>) {
    setValue(e.target.value)
    if (e.target.value.length > 3) {
      setIsLoading(true)
      setSearch(e.target.value)
    } else {
      setIsLoading(false)
      onClose()
    }
  }

  function handleFocus(e: FocusEvent<HTMLInputElement>) {
    if (options.length > 0) {
      onOpen()
    } else {
      onClose()
    }
  }

  function handleKey(e: KeyboardEvent<HTMLDivElement>) {
    if (e.code == "Escape") {
      onClose()
    }
  }

  function handleClick(e: MouseEvent<HTMLInputElement>) {
    if (options.length > 0) {
      onOpen()
    } else {
      onClose()
    }
  }

  function onSelect(option: SelectOptions) {
    setSelected(option)
    setSearch("")
  }
  switch (variant) {
    case SearchVariant.Outlined:
      return (
        <Container w="100%" m="0" p="0" maxH="200" mb={isOpen ? "5px" : "0px"} onKeyUp={(e) => handleKey(e)}>
          <HStack w="100%">
            <InputGroup>
              <Input
                name="value"
                autoComplete="off"
                value={value}
                w="100%"
                placeholder={placeholder ?? "Selecciona una opcion"}
                borderColor="brand.100"
                bg="white"
                _focus={{ borderColor: "brand.100" }}
                variant='filled'
                type="text"
                onChange={(e) => handleChange(e)}
                onFocus={(e) => handleFocus(e)}
                onClick={(e) => handleClick(e)}
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
                      selectd != null ?
                        <IoCloseOutline />
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
                mt="8px"
                maxH="150px"
                scrollBehavior="smooth"
                overflowY="scroll"
                boxShadow="xl"
                shadow="xl"
                css={{
                  '&::-webkit-scrollbar': {
                    width: '4px',
                  },
                  '&::-webkit-scrollbar-track': {
                    width: '6px',
                  },
                  '&::-webkit-scrollbar-thumb': {
                    background: "#323130",
                    borderRadius: '24px',
                  }
                }}
              >
                <Fade in={isOpen}>

                  {
                    options.length > 0 ?
                      options.map((value: SelectOptions, index: number) => {
                        return (
                          <ListItem
                            p="5px"
                            ml="4px"
                            mt="4px"
                            mb="4px"
                            cursor="pointer"
                            _hover={{ bg: "gray.200", cursor: "pointer" }}
                            borderRadius={4}
                            key={index}
                            onClick={() => onSelect(value)}

                          >
                            {value.label}
                          </ListItem>
                        )
                      })
                      :
                      <Box w="100%" p="10px" textAlign="center">
                        <Text> Sin resultados </Text>
                      </Box>
                  }
                </Fade>
              </List>
              :
              <>
              </>
          }
        </Container>
      )
    case SearchVariant.Basic:
      return (
        <Container w="100%" m="0" p="0" maxH="200" mb="5px" onKeyUp={(e) => handleKey(e)}>
          <HStack w="100%">
            <InputGroup>
              <Input
                name="value"
                autoComplete="off"
                value={value}
                w="100%"
                placeholder={placeholder ?? "Selecciona una opcion"}
                variant='filled'
                type="text"
                _focus={{ borderColor: "gray.100" }}
                onChange={(e) => handleChange(e)}
                onFocus={(e) => handleFocus(e)}
                onClick={(e) => handleClick(e)}
              />
              <InputRightElement 
              onClick={(e) => handleOpenClose(e)} 
              bg="gray.300" 
              color="black" 
              borderTopRightRadius={8} 
              borderBottomRightRadius={8}
              _hover={{cursor:"pointer"}}
              >
                {
                   isLoading
                   ?
                   <SlOptions />
                   :
                   isOpen ?
                     <SlArrowUp />
                     :
                     selectd != null ?
                       <IoCloseOutline />
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
                mt="8px"
                maxH="150px"
                scrollBehavior="smooth"
                overflowY="scroll"
                boxShadow="xl"
                shadow="xl"
                css={{
                  '&::-webkit-scrollbar': {
                    width: '4px',
                  },
                  '&::-webkit-scrollbar-track': {
                    width: '6px',
                  },
                  '&::-webkit-scrollbar-thumb': {
                    background: "#323130",
                    borderRadius: '24px',
                  }
                }}
              >
                <Fade in={isOpen}>

                  {
                    options.length > 0 ?
                      options.map((value: SelectOptions, index: number) => {
                        return (
                          <ListItem
                            p="5px"
                            ml="4px"
                            mt="4px"
                            mb="4px"
                            cursor="pointer"
                            _hover={{ bg: "gray.200", cursor: "pointer" }}
                            borderRadius={4}
                            key={index}
                            onClick={() => onSelect(value)}

                          >
                            {value.label}
                          </ListItem>
                        )
                      })
                      :
                      <Box w="100%" p="10px" textAlign="center">
                        <Text> Sin resultados </Text>
                      </Box>
                  }
                </Fade>
              </List>
              :
              <>
              </>
          }
        </Container>
      )
    default:
      return (
        <Container w="100%" m="0" p="0" maxH="200" mb="5px" onKeyUp={(e) => handleKey(e)}>
          <HStack w="100%">
            <InputGroup>
              <Input
                name="value"
                autoComplete="off"
                value={value}
                w="100%"
                placeholder={placeholder ?? "Selecciona una opcion"}
                borderColor="brand.100"
                bg="white"
                _focus={{ borderColor: "brand.100" }}
                variant='filled'
                type="text"
                onChange={(e) => handleChange(e)}
                onFocus={(e) => handleFocus(e)}
                onClick={(e) => handleClick(e)}
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
                mt="8px"
                maxH="150px"
                scrollBehavior="smooth"
                overflowY="scroll"
                boxShadow="xl"
                shadow="xl"
                css={{
                  '&::-webkit-scrollbar': {
                    width: '4px',
                  },
                  '&::-webkit-scrollbar-track': {
                    width: '6px',
                  },
                  '&::-webkit-scrollbar-thumb': {
                    background: "#323130",
                    borderRadius: '24px',
                  }
                }}
              >
                <Fade in={isOpen}>

                  {
                    options.length > 0 ?
                      options.map((value: SelectOptions, index: number) => {
                        return (
                          <ListItem
                            p="5px"
                            ml="4px"
                            mt="4px"
                            mb="4px"
                            cursor="pointer"
                            _hover={{ bg: "gray.200", cursor: "pointer" }}
                            borderRadius={4}
                            key={index}
                            onClick={() => onSelect(value)}

                          >
                            {value.label}
                          </ListItem>
                        )
                      })
                      :
                      <Box w="100%" p="10px" textAlign="center">
                        <Text> Sin resultados </Text>
                      </Box>
                  }
                </Fade>
              </List>
              :
              <>
              </>
          }
        </Container>
      )
  }
}
export default SearchSelect