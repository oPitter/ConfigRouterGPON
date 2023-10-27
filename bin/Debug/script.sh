#!/bin/bash
rm /etc/rc.button/reset
wget -c -P /etc/rc.button/ http://10.44.44.2/openwrt/st/WR-840Nv4/reset
chmod 755 /etc/rc.button/reset
opkg update
opkg install luci-ssl
opkg install miniupnpd
opkg install luci-app-upnp
/etc/init.d/miniupnpd enable
/etc/init.d/miniupnpd start
/etc/init.d/firewall restart
reboot
