<?php
namespace App\CharacterDatabase;

use SilverStripe\Admin\ModelAdmin;
use App\CharacterDatabase\CharacterHat;
use App\CharacterDatabase\CharacterTop;
use App\CharacterDatabase\CharacterEyes;
use App\CharacterDatabase\CharacterHair;
use App\CharacterDatabase\CharacterMouth;
use App\CharacterDatabase\CharacterBottom;
use App\CharacterDatabase\CharacterSkinColor;

/**
 * Class \App\Database\ExperienceAdmin
 *
 */
class CharacterAdmin extends ModelAdmin
{
    private static $managed_models = array (
        CharacterSkinColor::class,
        CharacterEyes::class,
        CharacterMouth::class,
        CharacterHair::class,
        CharacterBottom::class,
        CharacterTop::class,
        CharacterHat::class,
    );

    private static $url_segment = "character-parts";

    private static $menu_title = "Character Parts";

    private static $menu_icon = "app/client/icons/block-text.svg";
}
