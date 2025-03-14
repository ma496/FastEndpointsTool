'use client'
import { Button } from '@/components/ui/button'
import Image from 'next/image'
import Link from 'next/link'
import React, { useState } from 'react'
import {
  Bell,
  CircleX,
  Headphones,
  Info,
  LogOut,
  Menu,
  UserCog,
} from 'lucide-react'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import { Badge } from '@/components/ui/badge'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import { usePathname } from 'next/navigation'
import { ThemeToggle } from '@/components/theme-toggle'

const Header = () => {
  const pathName = usePathname()

  const toggleSidebar = () => {
    document.getElementById('sidebar')?.classList.toggle('open')
    document.getElementById('overlay')?.classList.toggle('open')
  }

  const [notifications, setNotifications] = useState([
    {
      id: 1,
      profile: 'avatar.svg',
      user: 'Brooklyn Simmons',
      message:
        'recommended this online shop to byu electronics, <strong class="text-black">Advantage Electric</strong>',
      time: '5 minutes ago',
      shop: 'Advantage Electric',
    },
    {
      id: 2,
      profile: 'avatar-two.svg',
      user: 'Sophia Williams',
      message:
        'invites you ABC.fig file with you, <strong class="text-black">check item now</strong>',
      time: '10 minutes ago',
      shop: 'New item',
    },
    {
      id: 3,
      profile: 'avatar-three.svg',
      user: 'Ava Davis',
      message:
        'changed <strong class="text-black">the cosmetic payment</strong> due date to Sunday 05 March 2023',
      time: '15 minutes ago',
      shop: 'New item',
    },
  ])

  const removeNotification = (id: number) => {
    setNotifications((prevNotifications) =>
      prevNotifications.filter((notification) => notification.id !== id),
    )
  }

  return (
    <header className="fixed inset-x-0 top-0 z-30 bg-white dark:bg-gray-950 px-4 py-[15px] shadow-sm lg:px-5">
      <div className="flex items-center justify-between gap-5">
        <Link href="/" className="inline-block shrink-0 lg:ml-2.5">
          <div className="flex items-center gap-2">
            <Image
              src="/images/icon.png"
              alt="Logo"
              width={34}
              height={34}
            />
            <span className="font-bold dark:text-white">Easy for Net</span>
          </div>
        </Link>

        <div className="inline-flex items-center gap-3 sm:gap-5">
          <div className="order-2 lg:order-none">
            <Popover>
              <PopoverTrigger asChild>
                <button
                  type="button"
                  className="relative duration-300 hover:opacity-80 dark:text-white"
                >
                  <Bell className="h-5 w-5" />
                  {!!notifications?.length && (
                    <Badge
                      variant={'primary'}
                      size={'number'}
                      className="absolute -right-0.5 -top-0.5 grid h-3 min-w-3 place-content-center px-1 text-[9px]"
                    >
                      {notifications?.length ?? 0}
                    </Badge>
                  )}
                </button>
              </PopoverTrigger>
              <PopoverContent
                sideOffset={12}
                className="mr-4 w-full max-w-80 divide-y divide-gray-300 dark:divide-gray-700 dark:bg-gray-900 p-0"
              >
                <div className="rounded-t-lg bg-gray-100 dark:bg-gray-800 p-3 text-black dark:text-white">
                  <h2 className="font-semibold leading-5">
                    Notifications
                  </h2>
                </div>
                <div className="max-h-64 divide-y divide-gray-300 overflow-y-auto">
                  {!notifications?.length ? (
                    <div className="!grid min-h-[255px] w-full min-w-72 place-content-center p-10 text-lg hover:!bg-transparent sm:min-w-80">
                      <div className="mx-auto mb-4 rounded-full text-primary">
                        <Info className="h-10 w-10" />
                      </div>
                      No data available.
                    </div>
                  ) : (
                    notifications.map((notification) => (
                      <div
                        key={notification.id}
                        className="flex items-start gap-3 px-3 py-5 hover:bg-gray-100"
                      >
                        <Link
                          href="#"
                          className="size-9 shrink-0 overflow-hidden rounded-lg"
                        >
                          <Image
                            alt="Profile"
                            width={36}
                            height={36}
                            className="h-full object-cover"
                            src={`/images/${notification.profile}`}
                          />
                        </Link>
                        <div className="grow space-y-2.5">
                          <p className="text-xs/5 font-medium text-gray">
                            <span className="font-bold text-black">
                              {notification.user}
                            </span>{' '}
                            <span
                              dangerouslySetInnerHTML={{
                                __html: notification.message,
                              }}
                            ></span>
                          </p>
                          <div className="flex flex-wrap items-center gap-2.5 text-xs/4 font-medium text-gray">
                            <span>
                              {notification.time}
                            </span>
                            <span className="size-1 shrink-0 rounded-full bg-primary"></span>
                            <span>
                              {notification.shop}
                            </span>
                          </div>
                        </div>
                        <button
                          type="button"
                          className="shrink-0 text-gray-500 transition hover:text-danger"
                          onClick={() =>
                            removeNotification(
                              notification.id,
                            )
                          }
                        >
                          <CircleX className="size-5" />
                        </button>
                      </div>
                    ))
                  )}
                </div>
                {!!notifications?.length && (
                  <div className="px-5 py-2.5">
                    <Button className="w-full">
                      Show All Notifications
                    </Button>
                  </div>
                )}
              </PopoverContent>
            </Popover>
          </div>
          <div className="order-2 lg:order-none">
            <ThemeToggle />
          </div>
          <div className="hidden lg:block">
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <div className="group flex cursor-pointer items-center gap-2.5 rounded-lg [&[data-state=open]>button>svg]:rotate-180">
                  <div className="size-8 shrink-0 overflow-hidden rounded-full">
                    <Image
                      src="/images/profile.png"
                      width={32}
                      height={32}
                      className="h-full w-full object-cover"
                      alt="Profile Img"
                    />
                  </div>
                </div>
              </DropdownMenuTrigger>
              <DropdownMenuContent
                align="end"
                sideOffset={12}
                className="min-w-[200px] space-y-1 rounded-lg p-1.5 text-sm font-medium"
              >
                <DropdownMenuItem className="p-0">
                  <Link
                    href="/setting"
                    className={`flex items-center gap-1.5 rounded-lg px-3 py-2 ${pathName === '/setting' && '!bg-gray-400 !text-black'}`}
                  >
                    <UserCog className="size-[18px] shrink-0" />
                    Profile
                  </Link>
                </DropdownMenuItem>
                <DropdownMenuItem className="p-0">
                  <Link
                    href="/contact-us"
                    className={`flex items-center gap-1.5 rounded-lg px-3 py-2 ${pathName === '/contact-us' && '!bg-gray-400 !text-black'}`}
                  >
                    <Headphones className="size-[18px] shrink-0" />
                    Help Center
                  </Link>
                </DropdownMenuItem>
                <DropdownMenuItem className="p-0">
                  <Link
                    href="/login"
                    className={`flex items-center gap-1.5 rounded-lg px-3 py-2 ${pathName === '/login' && '!bg-gray-400 !text-black'}`}
                  >
                    <LogOut className="size-[18px] shrink-0" />
                    Sign out
                  </Link>
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          </div>
          <button
            type="button"
            className="order-3 duration-300 hover:opacity-80 lg:hidden dark:text-white"
            onClick={toggleSidebar}
          >
            <Menu className="h-5 w-5" />
          </button>
        </div>
      </div>
    </header>
  )
}

export default Header
