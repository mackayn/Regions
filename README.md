# Regions
Allow the ability for in-page PartialView navigation

# Issues
Only supports one region per page
Parent page has to destroy region by hooking into IDestructible
Modal Navigation or Popups won't pass back parameters, parent page would have to pass them down as region isn't know to Prism currently.
